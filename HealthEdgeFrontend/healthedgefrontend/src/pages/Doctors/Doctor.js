import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import DoctorListCard from "../../components/doctors/DoctorListCard";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";

const Doctors = () => {
  const [doctors, setDoctors] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        const response = await axios.get("http://localhost:5239/doctors/get");
        setDoctors(response.data);
      } catch (error) {
        console.error("Failed to fetch doctors:", error);
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
              await axios.delete(`http://localhost:5239/doctors/delete/${doctorId}`);
              setDoctors(doctors.filter((doctor) => doctor.id !== doctorId));
            } catch (error) {
              console.error("Failed to delete doctor:", error);
            }
          },
        },
        {
          label: "No",
        },
      ],
    });
  };

  return (
    <div>
      {doctors.map((doctor) => (
        <DoctorListCard key={doctor.id} doctor={doctor} onEdit={handleEdit} onDelete={handleDelete} />
      ))}
    </div>
  );
};

export default Doctors;