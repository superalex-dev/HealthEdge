import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';
import DoctorCard from '../components/doctors/doctorCard/DoctorCard';
import styles from './DoctorsList.module.css';

const DoctorsList = () => { 
  const location = useLocation();
  const [doctors, setDoctors] = useState([]);

  console.log(doctors);

  useEffect(() => {
    if (location.state && location.state.doctors) {
      setDoctors(location.state.doctors);
    }
  }, [location.state], [location.state.specialization]);

  return (
    <div className={styles.grid}>
      {doctors.map(doctor => (
        <DoctorCard key={doctor.id} doctor={doctor} />
      ))}
    </div>
  );
};

export default DoctorsList;