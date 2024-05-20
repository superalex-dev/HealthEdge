import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import PatientCard from "../../components/patients/PatientCard";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";

const Patients = () => {
  const [patients, setPatients] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPatients = async () => {
      try {
        const response = await axios.get("http://localhost:5239/patients/get");
        setPatients(response.data);
      } catch (error) {
        console.error("Failed to fetch patients:", error);
      }
    };

    fetchPatients();
  }, []);

  const handleEdit = (patientId) => {
    navigate(`/edit-patient/${patientId}`);
  };

  const handleDelete = (patientId) => {
    confirmAlert({
      title: "Confirm to delete",
      message: "Are you sure you want to delete this patient?",
      buttons: [
        {
          label: "Yes",
          onClick: async () => {
            try {
              await axios.delete(
                `http://localhost:5239/patients/delete/${patientId}`
              );
              setPatients(
                patients.filter((patient) => patient.id !== patientId)
              );
            } catch (error) {
              console.error("Failed to delete patient:", error);
            }
          },
        },
        {
          label: "No",
          onClick: () => {},
        },
      ],
    });
  };

  const handleAdminRedirect = () => {
    navigate('/admin-dashboard');
  };

  return (
    <div className="patients-directory-container">
      <h1 className="patients-directory-heading" style={{"textAlign": "center"}}>Patients Directory</h1>
      <div className="center-button">
        <button className="admin-redirect" onClick={handleAdminRedirect}>
          Back to the admin dashboard
        </button>
      </div>
      <div>
        {patients.map((patient) => (
          <PatientCard
            key={patient.id}
            patient={patient}
            onEdit={handleEdit}
            onDelete={handleDelete}
          />
        ))}
      </div>
    </div>
  );
};

export default Patients;
