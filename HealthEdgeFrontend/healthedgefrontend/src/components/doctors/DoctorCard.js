import React, { useEffect, useState } from 'react';
import { StarIcon } from '@heroicons/react/solid';

const DoctorCard = ({ doctor }) => {
  const [earliestSlot, setEarliestSlot] = useState(null);
  const [specialization, setSpecialization] = useState(0);
  const [region, setRegion] = useState(0);
  const [insurance, setInsurance] = useState(0);

  console.log(doctor);

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
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // getMonth() is zero-based
    const year = date.getFullYear();
    if(year < 2000){
      return "Call for more information.";
    }
    const hours = date.getUTCHours() + 1;
    const minutes = date.getUTCMinutes().toString().padStart(2, '0');
    const suffix = hours >= 12 ? 'PM' : 'AM';
    const formattedHour = ((hours + 11) % 12 + 1); // Convert 24h to 12h format

    // Check if adding an hour crosses into the next day
    if (hours >= 24) {
      hours -= 24; // Adjust hour back to '0' if it was '24'
      date.setDate(date.getDate() + 1); // Increment the day
    }

    return `${day}.${month}.${year}, ${formattedHour}:${minutes} ${suffix}`;
  };

  let formattedDate = customFormatDate(earliestSlot);


  return (
    <div className="bg-white shadow-lg rounded-lg p-6 flex flex-col items-start">
      <img className="w-24 h-24 rounded-full mb-4" src={doctor.photoUrl} alt={doctor.firstName + ' ' + doctor.lastName} />
      <p className="text-sm text-gray-600">{specialization.name} - {region.name}</p>
      <div className="flex items-center my-2">
        {[...Array(5)].map((_, i) => (
          <StarIcon key={i} className="h-5 w-5 text-yellow-400" />
        ))}
        <span className="text-sm text-gray-600 ml-2">{`${doctor.ratings} оценки`}</span>
      </div>
      <p className="text-sm text-gray-500 mb-4">{`Най-ранен свободен час: ${formattedDate}`}</p>
      <button className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">
        Запази час
      </button>
    </div>
  );
};

export default DoctorCard;