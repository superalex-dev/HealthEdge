import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';
import DoctorCard from '../components/doctors/DoctorCard';

const DoctorsList = () => { 
  const location = useLocation();
  const [doctors, setDoctors] = useState([]);

  useEffect(() => {
    if (location.state && location.state.doctors) {
      setDoctors(location.state.doctors);
    }
  }, [location.state], [location.state.specialization]);

  console.log(doctors);

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      {doctors.map(doctor => (
        <DoctorCard key={doctor.id} doctor={doctor} />
      ))}
    </div>
  );
};

export default DoctorsList;