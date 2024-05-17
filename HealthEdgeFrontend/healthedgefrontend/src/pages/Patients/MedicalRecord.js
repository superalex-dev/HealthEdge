import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import "./MedicalRecord.css";
import { Button } from '@mui/joy';

const MedicalRecord = () => {
  const [medicalRecords, setMedicalRecords] = useState([]);
  const [patient, setPatient] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    const patientId = localStorage.getItem("patientId");

    const url = `http://localhost:5239/patients/${patientId}/medical-records`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setMedicalRecords(data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, []);

  useEffect(() => {
    const patientId = localStorage.getItem("patientId");

    const url = `http://localhost:5239/patients/get/${patientId}`;
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setPatient(data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  });

  console.log(medicalRecords);

  const customFormatDate = (dateString) => {
    const date = new Date(dateString);

    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();

    let hours = date.getHours();
    const minutes = String(date.getMinutes()).padStart(2, "0");

    const ampm = hours >= 12 ? "PM" : "AM";

    hours = hours % 12;
    hours = hours ? hours : 12;
    
    const formattedDate = `${day}.${month}.${year}, ${hours}:${minutes} ${ampm}`;

    return formattedDate;
  };

  const [doctorNames, setDoctorNames] = useState({});

  const getDoctorName = async (doctorId) => {
    const url = `http://localhost:5239/doctors/get/${doctorId}`;
    try {
      const response = await fetch(url);
      const doctor = await response.json();
      return `${doctor.firstName} ${doctor.lastName}`;
    } catch (error) {
      console.error("Error:", error);
      return "";
    }
  };

  useEffect(() => {
    const fetchDoctorNames = async () => {
      const names = {};
      for (const record of medicalRecords) {
        const name = await getDoctorName(record.doctorId);
        names[record.doctorId] = name;
      }
      setDoctorNames(names);
    };

    fetchDoctorNames();
  }, [medicalRecords]);

  return (
    <div className="container">
      <h1>My Medical Records</h1>
      <div className="records-container">
        {medicalRecords.map((record) => (
          <div key={record.id} className="record-card">
            <h2>Record Date: {customFormatDate(record.recordDate)}</h2>
            <p>
              <strong>Patient name:</strong>{" "}
              {patient.firstName + " " + patient.lastName}
            </p>
            <p>
              <strong>Doctor name:</strong> {doctorNames[record.doctorId]}
            </p>
            <p>
              <strong>Diagnosis:</strong> {record.diagnosis}
            </p>
            <p>
              <strong>Treatment:</strong> {record.treatment}
            </p>
          </div>
        ))}
        <Button color="success" style={{marginTop: "2rem"}} type="button" onClick={() => navigate('/home')}>Doctor or Admin account</Button>
      </div>
    </div>
  );
};

export default MedicalRecord;
