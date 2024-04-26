import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const SearchResultsComponent = () => {
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const location = useLocation();

  useEffect(() => {
    const fetchResults = async () => {
      try {
        const response = await axios.get(`http://localhost:5239/doctors/search${location.search}`);
        setResults(response.data);
        setLoading(false);
      } catch (error) {
        console.error('Failed to fetch:', error);
        setError('Failed to load results');
        setLoading(false);
      }
    };

    fetchResults();
  }, [location.search]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;
  return (
    <div>
      <h1>Search Results</h1>
      {results.length > 0 ? (
        results.map(doctor => (
          <div key={doctor.id}>
            <h2>{doctor.firstName} {doctor.lastName}</h2>
            <p>Specialization ID: {doctor.specializationId}</p>
            <p>Email: {doctor.email}</p>
          </div>
        ))
      ) : (
        <p>No results found.</p>
      )}
    </div>
  );
};

export default SearchResultsComponent;