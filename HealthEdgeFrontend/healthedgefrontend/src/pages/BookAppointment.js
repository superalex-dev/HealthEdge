import React from 'react';
import { Link } from 'react-router-dom';

const BookAppointment = () => {
  const formStyle = {
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    maxWidth: '400px',
    margin: '20px auto',
    padding: '20px',
    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
    borderRadius: '8px',
  };

  const inputStyle = {
    padding: '10px',
    borderRadius: '4px',
    border: '1px solid #ccc',
  };

  const buttonStyle = {
    padding: '10px 20px',
    border: 'none',
    borderRadius: '4px',
    backgroundColor: '#007bff',
    color: 'white',
    cursor: 'pointer',
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log("Form submitted");
  };

  return (
    <form onSubmit={handleSubmit} style={formStyle}>
      <input type="number" id="patientId" name="patientId" placeholder="Patient ID" required style={inputStyle} />
      <input type="number" id="doctorId" name="doctorId" placeholder="Doctor ID" required style={inputStyle} />
      <input type="date" id="recordDate" name="recordDate" required style={inputStyle} />
      <input type="date" id="appointmentDate" name="appointmentDate" required style={inputStyle} />
      <input type="number" id="roomNumber" name="roomNumber" placeholder="Room Number" required style={inputStyle} />
      <select id="isCancelled" name="isCancelled" style={inputStyle}>
        <option value="false">No</option>
        <option value="true">Yes</option>
      </select>
      <select id="isCompleted" name="isCompleted" style={inputStyle}>
        <option value="false">No</option>
        <option value="true">Yes</option>
      </select>
      <textarea id="diagnosis" name="diagnosis" placeholder="Diagnosis" required style={inputStyle}></textarea>
      <textarea id="treatment" name="treatment" placeholder="Treatment" required style={inputStyle}></textarea>
      <button type="submit" style={buttonStyle}>Book Appointment</button>
    </form>
  );
};


export default BookAppointment;