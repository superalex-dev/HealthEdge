import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const DoctorCard = ({ doctor }) => {
  const styles = {
    mainContent: {
      backgroundColor: "white",
      boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1)",
      borderRadius: "8px",
      padding: "24px",
      display: "flex",
      flexDirection: "column",
      alignItems: "start",
      position: "relative",
    },
    formContainer: {
      backgroundColor: "white",
      boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1)",
      borderRadius: "8px",
      padding: "24px",
      marginTop: "20px",
    },
    profileImage: {
      width: "300px",
      height: "300px",
      borderRadius: "1rem",
      marginBottom: "16px",
    },
    specializationRegion: {
      color: "#4b5563",
      fontSize: "14px",
    },
    starIcons: {
      display: "flex",
      alignItems: "center",
    },
    ratingsText: {
      marginLeft: "8px",
      color: "#4b5563",
      fontSize: "14px",
    },
    earliestSlot: {
      color: "#6b7280",
      fontSize: "14px",
      marginBottom: "16px",
    },
    actionButton: {
      backgroundColor: "#10b981",
      color: "white",
      padding: "8px 16px",
      borderRadius: "4px",
      cursor: "pointer",
      transition: "background-color 0.3s",
    },
    modalBackdrop: {
      position: "fixed",
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      backgroundColor: "rgba(0, 0, 0, 0.5)",
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      zIndex: 5000,
    },
    modalContent: {
      background: "white",
      padding: "20px",
      borderRadius: "8px",
      boxShadow: "0 4px 6px rgba(0, 0, 0, 0.25)",
    },
    modalHeading: {
      fontSize: "18px",
      fontWeight: "bold",
      marginBottom: "2rem",
    },
    modalButton: {
      backgroundColor: "#ef4444",
      color: "white",
      padding: "8px 16px",
      borderRadius: "4px",
      cursor: "pointer",
      transition: "background-color 0.3s",
    },
    timeSlot: {
      padding: "8px 16px",
      margin: "5px",
      backgroundColor: "#e5e7eb",
      borderRadius: "4px",
      cursor: "pointer",
    },
    timeSlotDisabled: {
      backgroundColor: "#9ca3af",
      color: "#374151",
      cursor: "not-allowed",
      opacity: "0.5",
    },
  };
  const formStyle = {
    formContainer: {
      marginTop: "1rem",
      border: "2px solid #ccc",
      backgroundColor: "white",
      boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1)",
      borderRadius: "8px",
      padding: "24px",
      display: "flex",
      flexDirection: "column",
      alignItems: "start",
      position: "relative",
      width: "90%",
    },
    label: {
      marginBottom: "8px",
    },
    input: {
      padding: "8px",
      marginBottom: "16px",
      border: "1px solid #ccc",
      borderRadius: "5px",
      width: "100%",
    },
    textarea: {
      padding: "8px",
      marginBottom: "16px",
      border: "1px solid #ccc",
      borderRadius: "5px",
      width: "100%",
      minHeight: "100px",
    },
    select: {
      padding: "8px",
      marginBottom: "16px",
      border: "1px solid #ccc",
      borderRadius: "5px",
      width: "100%",
      backgroundColor: "white",
      cursor: "pointer",
    },
    buttonContainer: {
      display: "flex",
      justifyContent: "center",
      marginTop: "20px",
    },
    button: {
      backgroundColor: "#007bff",
      color: "white",
      border: "none",
      padding: "10px 20px",
      borderRadius: "5px",
      cursor: "pointer",
      transition: "background-color 0.3s",
      marginRight: "10px",
    },
    cancelButton: {
      backgroundColor: "red",
      color: "white",
      border: "none",
      padding: "10px 20px",
      borderRadius: "5px",
      cursor: "pointer",
      transition: "background-color 0.3s",
    },
  };

  const navigate = useNavigate();
  const [earliestSlot, setEarliestSlot] = useState(null);
  const [specialization, setSpecialization] = useState(0);
  const [appointments, setAppointments] = useState([]);
  const [region, setRegion] = useState(0);
  const [insurances, setInsurances] = useState([]);
  const [reason, setReason] = useState();
  const [paymentMethod, setPaymentMethod] = useState("");
  const [notes, setAdditionalNotes] = useState();
  const [showDropdown, setShowDropdown] = useState(false);
  let [selectedDate, setSelectedDate] = useState("");
  const [showForm, setShowForm] = useState(false);

  const days = Array.from({ length: 5 }, (_, index) => {
    const day = new Date();
    day.setDate(day.getDate() + index);
    return day;
  });

  const timeSlots = [
    "10:30",
    "11:30",
    "12:30",
    "13:30",
    "14:30",
    "15:30",
    "16:30",
    "17:30",
  ];

  const handleSaveAppointment = () => {
    setShowForm(false);
  };

  useEffect(() => {
    const url = `http://localhost:5239/find-soonest-slot?doctorId=${doctor.id}`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setEarliestSlot(data.appointmentTime);
      })
      .catch((error) => {
        //console.error("Error:", error);
      });
  }, [doctor.id]);

  useEffect(() => {
    const ids = doctor.insuranceIds;
    
    ids.forEach(id => {
      const url = `http://localhost:5239/insurances/${id}`;
      fetch(url)
        .then((response) => response.json())
        .then((data) => {
          setInsurances(prev => [...prev, data.name]);
        })
        .catch((error) => {
          console.error("Error:", error);
        });
    
    })
  }, [doctor.id, doctor.insuranceIds]);

  console.log(insurances)

  useEffect(() => {
    const url = `http://localhost:5239/specializations/${doctor.specializationId}`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setSpecialization(data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, [doctor.specializationId]);

  useEffect(() => {
    const url = `http://localhost:5239/doctor/${doctor.id}`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setAppointments(data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, [doctor.id]);

  useEffect(() => {
    const url = `http://localhost:5239/regions/${doctor.regionId}`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setRegion(data);
      })
      .catch((error) => {
        console.error("error:", error);
      });
  }, [doctor.regionId]);

  const customFormatDate = (dateString) => {
    const date = new Date(dateString);
    const year = date.getFullYear();

    if (year < 2000) {
      return "Обадете се за повече информация.";
    }

    let localDate = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
    let hours = localDate.getHours();
    let minutes = localDate.getMinutes();

    if (hours === 17 && minutes > 30) {
      localDate.setHours(8, 30, 0, 0);
      localDate.setDate(localDate.getDate());
    }

    const day = localDate.getDate().toString().padStart(2, "0");
    const month = (localDate.getMonth() + 1).toString().padStart(2, "0");
    const formattedHour = ((localDate.getHours() + 11) % 12) + 2;
    const formattedMinutes = localDate.getMinutes().toString().padStart(2, "0");
    const suffix = localDate.getHours() >= 12 ? "PM" : "AM";

    return `${day}.${month}.${year}, ${formattedHour}:${formattedMinutes} ${suffix}`;
  };

  const formatDate = (date) => {
    const options = { weekday: "long", month: "long", day: "numeric" };
    const formattedDate = date.toLocaleDateString("bg-BG", options);
    return formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);
  };

  const handleSelect = (day, time) => {
    setSelectedDate(`${formatDate(day)}, ${time}`);
    setShowDropdown(false);
  };

  let formattedDate = customFormatDate(earliestSlot);

  const isTimeSlotTaken = (day, time) => {
    return appointments.some((appointment) => {
      const appointmentDate = new Date(appointment.appointmentTime);
      appointmentDate.setDate(appointmentDate.getDate());
      //appointmentDate.setDate(appointmentDate.getDate() - 1);
      const appointmentDay = appointmentDate.toISOString().split("T")[0];

      let dayInUTC = new Date(day).toISOString().split("T")[0];

      const localTime = new Date(
        appointmentDate.getTime() + appointmentDate.getTimezoneOffset() * 60000
      );
      const hours = localTime.getHours();
      const minutes = localTime.getMinutes();
      const formattedTime = `${hours < 10 ? "0" + hours : hours}:${
        minutes < 10 ? "0" + minutes : minutes
      }`;

      return dayInUTC === appointmentDay && time === formattedTime;
    });
  };

  const getPatientId = () => {
    const user = localStorage.getItem("patientId");
    return user;
  };

  const saveAppointment = () => {
    if (!selectedDate || !doctor) {
      return;
    }

    const dateInfo = parseBulgarianDate(selectedDate);
    const dateObject = createDateObject(2024, dateInfo);
    const formattedUTCDate = formatDateAsUTC(dateObject);

    setSelectedDate(formattedUTCDate);

    const patientId = getPatientId();

    const URL = `http://localhost:5239/create?PatientId=${patientId}&DoctorId=${doctor.id}&AppointmentTime=${formattedUTCDate}&Notes=${notes}&Reason=${reason}&PaymentMethod=${paymentMethod}`;
    console.log(URL);
    axios
      .post(URL)
      .then((response) => {
        console.log(response.data);
        navigate("/home");
      })
      .catch((error) => {
        console.error(error);
      });
  };

  const handleSaveAndClose = () => {
    handleSaveAppointment();
    saveAppointment();
  };

  function parseBulgarianDate(dateStr) {
    const bulgarianMonths = {
      януари: 0,
      февруари: 1,
      март: 2,
      април: 3,
      май: 4,
      юни: 5,
      юли: 6,
      август: 7,
      септември: 8,
      октомври: 9,
      ноември: 10,
      декември: 11,
    };

    const parts = dateStr.split(", ");
    const dayMonth = parts[1].split(" ");
    const day = parseInt(dayMonth[0]);
    const month = bulgarianMonths[dayMonth[1].toLowerCase()];
    const time = parts[2].split(":");
    const hours = parseInt(time[0]);
    const minutes = parseInt(time[1]);

    return { day, month, hours, minutes };
  }

  function createDateObject(year, dateInfo) {
    const date = new Date(
      Date.UTC(
        year,
        dateInfo.month,
        dateInfo.day,
        dateInfo.hours,
        dateInfo.minutes
      )
    );
    return date;
  }

  function formatDateAsUTC(date) {
    return date.toISOString();
  }

  //console.log(doctor);

  insurances.forEach((ins) => {
    if (/^\d+$/.test(ins)) {
      const URL = `http://localhost:5239/insurances/${ins}`;

      fetch(URL)
        .then((response) => response.json())
        .then((data) => {
          var index = insurances.indexOf(ins);
          insurances[index] = data.name;
        })
        .catch((error) => {
          console.error("Error:", error);
        });
    }
  });

  return (
    <div style={styles.mainContent}>
      <img
        style={styles.profileImage}
        src={doctor.imageUrl}
        alt={`${doctor.firstName} ${doctor.lastName} ID IS: ${doctor.id}`}
      />
      <h2>
        {doctor.firstName} {doctor.lastName}
      </h2>
      <p style={styles.specializationRegion}>
        <b>Телефон за контакт:</b> {doctor.contactNumber}
        <br></br>
        <b>Email адрес:</b> {doctor.email}
      </p>
      <p style={styles.specializationRegion}>
        <b>Регион: </b> {region.name}
        <br></br>
        <b>Специализация:</b> {specialization.name}
        <br></br>
        <b>Застрахователи: </b> {insurances.join(", ")}
      </p>
      <p
        style={styles.earliestSlot}
      >{`Най-ранен свободен час: ${formattedDate}`}</p>
      <button style={styles.actionButton} onClick={() => setShowDropdown(true)}>
        Запази час
      </button>
      {showDropdown && (
        <div style={styles.modalBackdrop}>
          <div style={styles.modalContent}>
            <h2 style={styles.modalHeading}>Изберете дата и час</h2>
            {days.map((day) => (
              <div key={day}>
                <h3 style={{ textAlign: "center" }}>{formatDate(day)}</h3>
                {timeSlots.map((time) => (
                  <button
                    key={`${day}-${time}`}
                    style={{
                      ...styles.timeSlot,
                      ...(isTimeSlotTaken(day, time) &&
                        styles.timeSlotDisabled),
                    }}
                    onClick={() =>
                      !isTimeSlotTaken(day, time) && handleSelect(day, time)
                    }
                    disabled={isTimeSlotTaken(day, time)}
                  >
                    {time}
                  </button>
                ))}
              </div>
            ))}
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                marginTop: "1rem",
              }}
            >
              <button
                style={styles.modalButton}
                onClick={() => setShowDropdown(false)}
              >
                Затвори
              </button>
            </div>
          </div>
        </div>
      )}
      {selectedDate && (
        <>
          <p>{`Избрано: ${selectedDate}`}</p>
          <button
            style={styles.actionButton}
            onClick={() => setShowForm(true)}
          >
            Запази
          </button>
        </>
      )}

      {showForm && (
        <div style={formStyle.formContainer}>
          <h2>Форма за записване на час</h2>
          <label style={formStyle.label} htmlFor="reason">
            Причина:
          </label>
          <input
            style={formStyle.input}
            type="text"
            id="reason"
            placeholder="Болки в кръста, болки в гърлото..."
            value={reason}
            onChange={(e) => setReason(e.target.value)}
          />

          <label style={formStyle.label} htmlFor="additionalNotes">
            Допълнителни бележки към доктора:
          </label>
          <textarea
            style={formStyle.textarea}
            id="additionalNotes"
            value={notes}
            placeholder="Приемам и други лекарства, алергичен съм към..."
            onChange={(e) => setAdditionalNotes(e.target.value)}
          />

          <label style={formStyle.label} htmlFor="paymentMethod">
            Метод на плащане:
          </label>
          <select
            style={formStyle.select}
            id="paymentMethod"
            value={paymentMethod}
            placeholder="Приемам и други лекарства, алергичен съм към..."
            onChange={(e) => setPaymentMethod(e.target.value)}
          >
            <option value="">Избери начин на плащане...</option>
            <option value="cash">В брой</option>
            <option value="card">С карта</option>
          </select>

          <div style={formStyle.buttonContainer}>
            <button style={formStyle.button} onClick={handleSaveAndClose}>
              Запази
            </button>
            <button
              style={formStyle.cancelButton}
              onClick={() => setShowForm(false)}
            >
              Отказ
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default DoctorCard;
