import React, { useState, useEffect } from 'react';
import axios from 'axios';

const HomePage = () => {
  const [search, setSearch] = useState({
    specialization: '',
    city: '',
    name: ''
  });
  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);

  useEffect(() => {
    const fetchSpecializationsAndCities = async () => {
      try {
        const specResponse = await axios.get('http://localhost:5239/doctors/specializations');
        setSpecializations(specResponse.data);
        
        const cityResponse = await axios.get('http://localhost:5239/doctors/cities');
        setCities(cityResponse.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchSpecializationsAndCities();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setSearch(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Implementation of search functionality goes here
    console.log('Search criteria:', search);
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Find Your Doctor</h1>
      <form onSubmit={handleSubmit} className="mb-4">
        <div className="mb-2">
          <label htmlFor="specialization" className="block mb-1">Specialization:</label>
          <select
            name="specialization"
            value={search.specialization}
            onChange={handleInputChange}
            className="border border-gray-300 rounded p-2 w-full"
          >
            <option value="">Select specialization</option>
            {specializations.map((spec, index) => (
              <option key={index} value={spec}>{spec}</option>
            ))}
          </select>
        </div>
        <div className="mb-2">
          <label htmlFor="city" className="block mb-1">City:</label>
          <select
            name="city"
            value={search.city}
            onChange={handleInputChange}
            className="border border-gray-300 rounded p-2 w-full"
          >
            <option value="">Select city</option>
            {cities.map((city, index) => (
              <option key={index} value={city}>{city}</option>
            ))}
          </select>
        </div>
        <div className="mb-2">
          <label htmlFor="name" className="block mb-1">Doctor's Name:</label>
          <input
            type="text"
            name="name"
            value={search.name}
            onChange={handleInputChange}
            placeholder="Enter doctor's name"
            className="border border-gray-300 rounded p-2 w-full"
          />
        </div>
        <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
          Search
        </button>
      </form>
    </div>
  );
};

export default HomePage;