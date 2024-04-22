import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const SearchResults = () => {
  const [doctors, setDoctors] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [specializations, setSpecializations] = useState([]);
  const [regions, setRegions] = useState([]);
  const location = useLocation();

  useEffect(() => {
    const fetchSpecializationsAndRegions = async () => {
      try {
        const [specializationsResponse, regionsResponse] = await Promise.all([
          axios.get('http://localhost:5239/specializations'),
          axios.get('http://localhost:5239/regions')
        ]);
        setSpecializations(specializationsResponse.data);
        setRegions(regionsResponse.data);
      } catch (error) {
        console.error('Error fetching specializations and regions:', error);
      }
    };

    fetchSpecializationsAndRegions();
  }, []);

  const fetchDoctors = async (search) => {
    setLoading(true);
    const { specialization, city, name = "" } = search;
    const selectedSpecialization = specializations.find(s => s.name === specialization);
    const selectedCity = regions.find(c => c.name === city);
    if (!selectedSpecialization || !selectedCity) {
      console.error('Invalid search parameters:', search);
      setError('Invalid search parameters. Please try again.');
      setLoading(false);
      return;
    }
    try {
      let params = {
        specializationId: selectedSpecialization.id,
        regionId: selectedCity.id
      };
      if (name) {
        params.name = name;
      }
      const response = await axios.get('http://localhost:5239/doctors/search', { params });
      setDoctors(response.data);
    } catch (error) {
      console.error('Error fetching data:', error);
      setError('Failed to fetch data. Please try again later.');
    }
    setLoading(false);
  };

  useEffect(() => {
    if (location.state) {
      fetchDoctors(location.state.search);
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