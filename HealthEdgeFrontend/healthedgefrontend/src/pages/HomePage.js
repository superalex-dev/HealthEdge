import React, { useState, useEffect } from 'react';
import axios from 'axios';
import DoctorSearchComponent from '../components/doctors/DoctorSearchComponent';
import { useNavigate } from 'react-router-dom';
import superHeroDoctor from '../assets/superhealthyedge.png';

const HomePage = () => {
  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);
  const [insurances, setInsurances] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    axios.all([
      axios.get('http://localhost:5239/doctors/specializations'),
      axios.get('http://localhost:5239/doctors/cities'),
      axios.get('http://localhost:5239/doctors/insurances')
    ]).then(axios.spread((specData, cityData, insuranceData) => {
      setSpecializations(specData.data);
      setCities(cityData.data);
      setInsurances(insuranceData.data);
      setLoading(false);
    })).catch(error => {
      console.error('Failed to fetch data:', error);
      setError('Failed to fetch data');
      setLoading(false);
    });
  }, []);

  const handleSearch = (searchParams) => {
    const query = new URLSearchParams(searchParams).toString();
    navigate(`/search-results?${query}`);
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col items-center justify-center">
      <div className="bg-white p-6 rounded-lg shadow-lg">
        <img src={superHeroDoctor} alt="Superhero Doctor" className="h-32 mx-auto" />
        <h1 className="text-xl font-semibold text-center text-gray-800 mb-4">Find Your Doctor</h1>
        <DoctorSearchComponent
          specializations={specializations}
          cities={cities}
          insurances={insurances}
          onSearch={handleSearch}
        />
      </div>
    </div>
  );
};

export default HomePage;