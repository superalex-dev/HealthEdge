import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { SearchIcon } from '@heroicons/react/outline';

const HomePage = () => {
  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);
  const [doctors, setDoctors] = useState([]);
  const [searchParams, setSearchParams] = useState({
    specialization: '',
    city: '',
    pediatrician: false
  });

  useEffect(() => {
    const fetchFilters = async () => {
      try {
        const specResponse = await axios.get('http://localhost:5239/doctors/specializations');
        const cityResponse = await axios.get('http://localhost:5239/doctors/cities');
        setSpecializations(specResponse.data);
        setCities(cityResponse.data);
      } catch (error) {
        console.error('Error fetching filter options:', error);
      }
    };
    fetchFilters();
  }, []);

  const handleSearch = async () => {
    try {
      const response = await axios.get('http://localhost:5239/doctors/search', {
        params: {
          specialization: searchParams.specialization,
          needsToBeAPediatrician: searchParams.pediatrician,
          cityPreference: searchParams.city
        }
      });
      setDoctors(response.data);
    } catch (error) {
      console.error('Failed to fetch doctors:', error);
    }
  };

  return (
    <div className="bg-white min-h-screen flex flex-col justify-center">
      <div className="max-w-md mx-auto rounded-lg overflow-hidden md:max-w-lg">
        <div className="md:flex">
          <div className="w-full p-4">
            <div className="relative">
              <span className="absolute inset-y-0 left-0 flex items-center pl-2">
                <SearchIcon className="h-5 w-5 text-gray-500" />
              </span>
              <input 
                type="text" 
                className="py-2 text-sm text-white bg-gray-200 rounded-md pl-10 focus:outline-none focus:bg-white focus:text-gray-900" 
                placeholder="Search..." 
                autoComplete="off"
              />
            </div>
            <div className="mt-4">
              <label className="block" htmlFor="location">Location</label>
              <select 
                id="location" 
                className="w-full mt-2 border rounded-md"
                value={searchParams.city}
                onChange={e => setSearchParams({ ...searchParams, city: e.target.value })}
              >
                {cities.map((city, index) => (
                  <option key={index} value={city}>{city}</option>
                ))}
              </select>
            </div>
            <div className="mt-4">
              <label className="block" htmlFor="specialization">Specialization</label>
              <select 
                id="specialization" 
                className="w-full mt-2 border rounded-md"
                value={searchParams.specialization}
                onChange={e => setSearchParams({ ...searchParams, specialization: e.target.value })}
              >
                {specializations.map((spec, index) => (
                  <option key={index} value={spec}>{spec}</option>
                ))}
              </select>
            </div>
            <div className="mt-4 flex items-center">
              <input 
                id="pediatrician" 
                type="checkbox"
                checked={searchParams.pediatrician}
                onChange={(e) => setSearchParams({ ...searchParams, pediatrician: e.target.checked })}
                className="mr-2"
              />
              <label htmlFor="pediatrician">Pediatrician only</label>
            </div>
            <button 
              className="mt-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              onClick={handleSearch}
            >
              Search
            </button>
            {doctors.length > 0 && (
              <div className="mt-4">
                <h3 className="text-lg font-semibold">Doctors:</h3>
                <ul>
                  {doctors.map((doctor, index) => (
                    <li key={index}>{doctor.firstName} {doctor.lastName} - {doctor.specialization}</li>
                  ))}
                </ul>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomePage;