import React, { useState, useEffect } from 'react';
import axios from 'axios';
import superHeroDoctor from '../assets/superhealthyedge.png';
import './HomePage.css';

const HomePage = () => {
  const [search, setSearch] = useState({ specialization: '', city: '', name: '' });
  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchSpecializationsAndCities = async () => {
      setLoading(true);
      try {
        const [specResponse, cityResponse] = await Promise.all([
          axios.get('http://localhost:5239/doctors/specializations'),
          axios.get('http://localhost:5239/doctors/cities')
        ]);
        setSpecializations(specResponse.data);
        setCities(cityResponse.data);
      } catch (error) {
        console.error('Error fetching data:', error);
        setError('Failed to fetch data. Please try again later.');
      }
      setLoading(false);
    };

    fetchSpecializationsAndCities();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setSearch(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Search criteria:', search);
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="homepage">
      <div className="logo-container">
        <img src={superHeroDoctor} alt="Superhero Doctor" className="logo" />
      </div>
      <div className="content">
        <h1 className="title">Find Your Doctor</h1>
        <form onSubmit={handleSubmit} className="search-form">
          <div className="input-group">
            <label htmlFor="specialization">Specialization:</label>
            <select name="specialization" value={search.specialization} onChange={handleInputChange}>
              <option value="">Select specialization</option>
              {specializations.map((spec, index) => (
                <option key={index} value={spec}>{spec}</option>
              ))}
            </select>
          </div>
          <div className="input-group">
            <label htmlFor="city">City:</label>
            <select name="city" value={search.city} onChange={handleInputChange}>
              <option value="">Select city</option>
              {cities.map((city, index) => (
                <option key={index} value={city}>{city}</option>
              ))}
            </select>
          </div>
          <div className="input-group">
            <label htmlFor="name">Doctor's Name:</label>
            <input type="text" name="name" value={search.name} onChange={handleInputChange} placeholder="Enter doctor's name" />
          </div>
          <button type="submit" className="search-button">Search</button>
        </form>
      </div>
    </div>
  );
};

export default HomePage;