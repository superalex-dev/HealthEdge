import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DoctorCard from "../../components/doctors/doctorCard/DoctorCard";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";
import "../Doctors/Doctors.css"

const Doctors = () => {
  const [doctors, setDoctors] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        const response = await axios.get("http://localhost:5239/doctors/get");
        setDoctors(response.data);
      } catch (error) {
        console.error("Failed to fetch patients:", error);
      }
    };

    fetchDoctors();
  }, []);

  const handleEdit = (doctorId) => {
    navigate(`/edit-doctor/${doctorId}`);
  };

  const handleDelete = (doctorId) => {
    confirmAlert({
      title: "Confirm to delete",
      message: "Are you sure you want to delete this doctor?",
      buttons: [
        {
          label: "Yes",
          onClick: async () => {
            try {
              await axios.delete(
                `http://localhost:5239/doctors/delete/${doctorId}`
              );
              setDoctors(
                doctors.filter((doctor) => doctor.id !== doctorId)
              );
            } catch (error) {
              console.error("Failed to delete doctor:", error);
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

  const handleViewDetails = (doctorId) => {
    navigate(`/doctor-details/${doctorId}`);
  };

  return (
    <div className="doctors-directory-container">
      <h1 className="doctors-directory-heading">Doctors Directory</h1>
      <div>
        {doctors.map((doctor) => (
          <div className="doctor-card-container">
          <div className="flex justify-between items-center">
            <div>
              <h3>{`${doctor.firstName} ${doctor.lastName}`}</h3>
              <p><b>Email: </b>{doctor.email}</p>
              <p><b>Phone number: </b>{doctor.contactNumber}</p>
            </div>
            <div className="flex items-center doctor-card-buttons">
              <button onClick={() => handleViewDetails(doctor.id)} aria-label="View doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
                View
              </button>
              <button onClick={() => handleEdit(doctor.id)} aria-label="Edit doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
                Edit
              </button>
              <button onClick={() => handleDelete(doctor.id)} aria-label="Delete doctor" style={{backgroundColor: "#4CAF50", color: "white"}}>
                Delete
              </button>
            </div>
          </div>
        </div>
        ))}
      </div>
    </div>
  );
};

export default Doctors;
