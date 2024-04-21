import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const SearchResults = () => {
  const [doctors, setDoctors] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const location = useLocation();

  const fetchDoctors = async (search) => {
    setLoading(true);
    const { specialization, city } = search;
    try {
      const response = await axios.get('http://localhost:5239/doctors/search', {
        params: {
          specializationId: specialization,
          regionId: city
        }
      });
      setDoctors(response.data);
    } catch (error) {
      console.error('Error fetching data:', error);
      setError('Failed to fetch data. Please try again later.');
    }
    setLoading(false);
  };  

  useEffect(() => {
    if (location.state) {
      const search = location.state.search;
      fetchDoctors(search);
    }
  }, [location.state]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h1>Search Results</h1>
      {doctors.map(doctor => (
        <div key={doctor.id}>
          <h2>{doctor.name}</h2>
          <p>{doctor.specialization}</p>
          <p>{doctor.city}</p>
          <p>{doctor.insurance}</p>
        </div>
      ))}
    </div>
  );
};

export default SearchResults;