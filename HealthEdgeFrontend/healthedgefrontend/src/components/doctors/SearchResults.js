import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const SearchResults = () => {
  const [doctors, setDoctors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const queryParams = {
      specializationId: params.get('specializationId'),
      regionId: params.get('regionId'),
      insuranceId: params.get('insuranceId'),
      firstName: params.get('firstName'),
      lastName: params.get('lastName')
    };
  
    console.log("Received Query Params:", queryParams);
  
    if (!queryParams.specializationId && !queryParams.regionId && !queryParams.insuranceId && !queryParams.firstName && !queryParams.lastName) {
      setError('At least one search parameter must be provided.');
      setLoading(false);
      return;
    }
  
    const fetchURL = `http://localhost:5239/doctors/search?${params.toString()}`;
    console.log("Fetching data from:", fetchURL);
  
    axios.get(fetchURL)
      .then(response => {
        setDoctors(response.data);
        setError('');
      })
      .catch(err => {
        console.error('Error fetching data:', err);
        setError(err.response?.data?.message || 'Failed to fetch data. Please try again later.');
      })
      .finally(() => {
        setLoading(false);
      });
  
  }, [location.search]);  

  function buildSearchParams(params) {
    const searchParams = {};
    ['specializationId', 'regionId', 'insuranceId', 'firstName', 'lastName', 'needsToBeAPediatrician'].forEach(key => {
      const value = params.get(key);
      if (value !== null) searchParams[key] = key === 'needsToBeAPediatrician' ? value === 'true' : value;
    });
    return searchParams;
  }

  function isValidSearchParams(searchParams) {
    return Object.values(searchParams).some(x => x !== null && x !== '');
  }

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div>
      <h1>Search Results</h1>
      {doctors.length > 0 ? (
        doctors.map(doctor => (
          <div key={doctor.id}>
            <h2>{doctor.name}</h2>
            <p>Specialization: {doctor.specialization}</p>
            <p>Location: {doctor.city}, {doctor.region}</p>
            <p>Insurance Accepted: {doctor.insurance}</p>
          </div>
        ))
      ) : (
        <div>No doctors found matching your criteria.</div>
      )}
    </div>
  );
};

export default SearchResults;