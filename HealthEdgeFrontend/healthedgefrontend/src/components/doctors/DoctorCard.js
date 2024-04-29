import React, { useEffect, useState } from 'react';
import { StarIcon } from '@heroicons/react/solid';
import './doctorCard.css'
import zIndex from '@mui/material/styles/zIndex';
import axios from 'axios';

const DoctorCard = ({ doctor }) => {
  const [earliestSlot, setEarliestSlot] = useState(null);
  const [specialization, setSpecialization] = useState(0);
  const [appointments, setAppointments] = useState([]);
  const [region, setRegion] = useState(0);
  const [insurance, setInsurance] = useState(0);
  const [showDropdown, setShowDropdown] = useState(false);
  let [selectedDate, setSelectedDate] = useState('');

  //console.log(doctor);

  const days = Array.from({ length: 7 }, (_, index) => {
    const day = new Date();
    day.setDate(day.getDate() + index);
    return day;
  });

  console.log(days);

  // Times slots from 8:30 AM to 5:30 PM
  const timeSlots = ['8:30', '9:30', '10:30', '11:30', '12:30', '13:30', '14:30', '15:30', '16:30', '17:30'];

  useEffect(() => {
    const url = `http://localhost:5239/find-soonest-slot?doctorId=${doctor.id}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        setEarliestSlot(data.appointmentTime);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }, [doctor.id]);

  useEffect(() => {
    const url = `http://localhost:5239/specializations/${doctor.specializationId}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        setSpecialization(data);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }, [doctor.specializationId]);

  useEffect(() => {
    const url = `http://localhost:5239/doctor/${doctor.id}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        setAppointments(data);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }, [doctor.id]);

  console.log(appointments);

  useEffect(() => {
    const url = `http://localhost:5239/regions/${doctor.regionId}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        setRegion(data);
      })
      .catch(error => {
        console.error('error:', error);
      });
  }, [doctor.regionId]);
  
  useEffect(() => {
    const url = `http://localhost:5239/insurances/${doctor.insuranceId}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        setInsurance(data);
      })
      .catch(error => {
        console.error('error:', error);
      });
  }, [doctor.insuranceId]);  

const customFormatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();

  if (year < 2000) {
    return "Call for more information.";
  }

  let hours = date.getUTCHours() + 1;
  let minutes = date.getUTCMinutes();

  if (hours > 17 || (hours === 17 && minutes > 30)) {

    date.setUTCDate(date.getUTCDate() + 1);
    hours = 8;
    minutes = 30;
  }

  const day = date.getUTCDate().toString().padStart(2, '0');
  const month = (date.getUTCMonth() + 1).toString().padStart(2, '0');
  const formattedHour = ((hours + 11) % 12 + 1);
  const formattedMinutes = minutes.toString().padStart(2, '0');
  const suffix = hours >= 12 ? 'PM' : 'AM';

  return `${day}.${month}.${year}, ${formattedHour}:${formattedMinutes} ${suffix}`;
};

const formatDate = (date) => {
  const options = { weekday: 'long', month: 'long', day: 'numeric' };
  return date.toLocaleDateString('bg-BG', options);
};

const handleSelect = (day, time) => {
  setSelectedDate(`${formatDate(day)}, ${time}`);
  setShowDropdown(false);
};

  let formattedDate = customFormatDate(earliestSlot);
  
  const isTimeSlotTaken = (day, time) => {
    return appointments.some(appointment => {
      const appointmentDate = new Date(appointment.appointmentTime);
      appointmentDate.setDate(appointmentDate.getDate() - 1); 
      const appointmentDay = appointmentDate.toISOString().split('T')[0];
  
      let dayInUTC = new Date(day).toISOString().split('T')[0];
  
      const localTime = new Date(appointmentDate.getTime() + (appointmentDate.getTimezoneOffset() * 60000));
      const hours = localTime.getHours();
      const minutes = localTime.getMinutes();
      const formattedTime = `${hours < 10 ? '0' + hours : hours}:${minutes < 10 ? '0' + minutes : minutes}`;
  
    //  console.log(`Comparing: ${dayInUTC} === ${appointmentDay} && ${time} === ${formattedTime}`);
  
      return dayInUTC === appointmentDay && time === formattedTime;
    });
  };

  const getPatientId = () => {
    const user = localStorage.getItem('patientId');
    return user;
  };

  const saveAppointment = () => {
    if (!selectedDate || !doctor) {
      return;
    }

    const dateInfo = parseBulgarianDate(selectedDate);
    const dateObject = createDateObject(2024, dateInfo);
    const formattedUTCDate = formatDateAsUTC(dateObject);

    selectedDate = (formattedUTCDate);

    const patientId = getPatientId();

    console.log(patientId);

  const URL = `http://localhost:5239/create?PatientId=${patientId}&DoctorId=53&AppointmentTime=${selectedDate}&Notes=sdsd&Status=ssdsd&Reason=sdsdsds&PaymentMethod=dsdsds`;
console.log(URL);
  axios.post(URL)
     .then(response => {
       console.log(response.data);
     })
     .catch(error => {
       console.error(error);
     });
  };

  function parseBulgarianDate(dateStr) {
    const bulgarianMonths = {
      'януари': 0, 'февруари': 1, 'март': 2, 'април': 3, 'май': 4,
      'юни': 5, 'юли': 6, 'август': 7, 'септември': 8, 'октомври': 9,
      'ноември': 10, 'декември': 11
    };
  
    const parts = dateStr.split(', ');
    const dayMonth = parts[1].split(' ');
    const day = parseInt(dayMonth[0]);
    const month = bulgarianMonths[dayMonth[1].toLowerCase()];
    const time = parts[2].split(':');
    const hours = parseInt(time[0]);
    const minutes = parseInt(time[1]);
  
    return { day, month, hours, minutes };
  }

  function createDateObject(year, dateInfo) {
    const date = new Date(Date.UTC(year, dateInfo.month, dateInfo.day, dateInfo.hours, dateInfo.minutes));
    return date;
  }

  function formatDateAsUTC(date) {
    return date.toISOString();
  }

  return (
    <div className="main-content bg-white shadow-lg rounded-lg p-6 flex flex-col items-start relative">
      <img className="w-24 h-24 rounded-full mb-4" src={doctor.photoUrl} alt={`${doctor.firstName} ${doctor.lastName} ${doctor.id}`} />
      <p className="text-sm text-gray-600">{specialization.name} - {region.name}</p>
      <div className="flex items-center my-2">
        {[...Array(5)].map((_, i) => (
          <StarIcon key={i} className="h-5 w-5 text-yellow-400" />
        ))}
        <span className="text-sm text-gray-600 ml-2">{`${doctor.ratings} оценки`}</span>
      </div>
      <p className="text-sm text-gray-500 mb-4">{`Най-ранен свободен час: ${formattedDate}`}</p>
      <button className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600" onClick={() => setShowDropdown(true)}>
        Запази час
      </button>
      {showDropdown && (
          <div className="modal-backdrop" style={{ zIndex: 5000 }}>
            <div className="modal-content">
              <h2 className="text-lg font-semibold">Изберете дата и час</h2>
              {days.map(day => (
                <div key={day} className="space-y-2">
                  <h3 className="px-4 py-2 text-gray-800 font-medium">{formatDate(day)}</h3>
                  {timeSlots.map(time => (
                    <button key={`${day}-${time}`}
                      className={`px-4 py-2 ${isTimeSlotTaken(day, time) ? 'bg-gray-400 text-gray-800 cursor-not-allowed opacity-50' : 'bg-gray-200 hover:bg-gray-300'}`}
                      onClick={() => !isTimeSlotTaken(day, time) && handleSelect(day, time)}
                      disabled={isTimeSlotTaken(day, time)}
                      style={{margin: 5}}>
                      {time}
                    </button>
                  ))}
                </div>
              ))}
              <button className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600" onClick={() => setShowDropdown(false)}>Затвори</button>
            </div>
          </div>
        )}
      {selectedDate && (
        <>
          <p className="text-sm text-green-500 mt-2">{`Избрано: ${selectedDate}`}</p>
          <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600" onClick={saveAppointment}>
            Запази
          </button>
        </>
      )}
    </div>
  );
  
};

export default DoctorCard;