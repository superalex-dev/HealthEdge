import React, { useState, useEffect } from 'react';
import axios from 'axios';
import DoctorCard from '../components/doctors/DoctorCard';

const DoctorsList = () => {
  const [doctors, setDoctors] = useState([]);

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        const response = await axios.get('http://localhost:5239/doctors/search');
        setDoctors(response.data);
      } catch (error) {
        console.error("Failed to fetch doctors:", error);
      }
    };

    fetchDoctors();
  }, []);

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      {doctors.map(doctor => (
        <DoctorCard key={doctor.id} doctor={doctor} />
      ))}
    </div>
  );
};

export default DoctorsList;