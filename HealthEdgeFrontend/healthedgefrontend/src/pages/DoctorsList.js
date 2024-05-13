import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';
import DoctorCard from '../components/doctors/doctorCard/DoctorCard';
import styles from './DoctorsList.module.css';

const DoctorsList = () => { 
  const location = useLocation();
  const [doctors, setDoctors] = useState([]);

  useEffect(() => {
    if (location.state && location.state.doctors) {
      setDoctors(location.state.doctors);
    }
  }, [location.state], [location.state.specialization]);

  return (
    <div>
      <div className={styles.header}>
        <h1>Search Results</h1>
      </div>
      <div className={styles.grid}>
        {doctors.map(doctor => (
          <div key={doctor.id} className={styles.smallDoctorCard}>
            <DoctorCard doctor={doctor} />
          </div>
        ))}
      </div>
    </div>
  );
  
};

export default DoctorsList;