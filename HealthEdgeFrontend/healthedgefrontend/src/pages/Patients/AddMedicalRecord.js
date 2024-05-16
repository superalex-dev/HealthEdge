import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import axios from "axios";
import "./AddMedicalRecord.css";

const AddMedicalRecord = () => {
  const location = useLocation();
  const [patientID, setPatientId] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    if (location.state && location.state.patientId) {
      setPatientId(location.state.patientId);
    }
  }, [location.state]);

  const [formData, setFormData] = useState({
    doctorId: "",
    patientId: location.state.patientId,
    recordData: "",
    diagnosis: "",
    treatment: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  console.log(patientID);

  const handleSubmit = (e) => {
    e.preventDefault();

    const submit = async () => {
      try {
        const URL = `http://localhost:5239/patients/${patientID}/medical-records/add`;

        console.log(formData);

        const response = await fetch(URL, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(formData),
        });

        navigate("/patients");
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    submit();
  };

  return (
    <div className="form-container">
      <h1>Add Medical Record</h1>
      <form onSubmit={handleSubmit} className="medical-record-form">
        <label htmlFor="patientId">Patient ID:</label>
        <input
          type="text"
          id="patientId"
          name="patientId"
          value={patientID}
          onChange={handleChange}
          required
        />

        <label htmlFor="doctorId">Doctor ID:</label>
        <input
          type="text"
          id="doctorId"
          name="doctorId"
          value={formData.doctorId}
          onChange={handleChange}
          required
        />

        <label htmlFor="diagnosis">Diagnosis:</label>
        <input
          type="text"
          id="diagnosis"
          name="diagnosis"
          value={formData.diagnosis}
          onChange={handleChange}
          required
        />

        <label htmlFor="treatment">Treatment:</label>
        <input
          type="text"
          id="treatment"
          name="treatment"
          value={formData.treatment}
          onChange={handleChange}
          required
        />

        <button type="submit">Add Record</button>
      </form>
    </div>
  );
};

export default AddMedicalRecord;
