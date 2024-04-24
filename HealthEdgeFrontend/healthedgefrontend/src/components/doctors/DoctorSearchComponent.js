import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SearchResultsComponent = () => {
  const location = useLocation();
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchResults = async () => {
      setLoading(true);
      setError('');
      try {
        const response = await axios.get(`http://localhost:5239/doctors/search${location.search}`);
        setResults(response.data);
      } catch (err) {
        setError('Failed to fetch results. Please try again later.');
        console.error('Error fetching search results:', err);
      }
      setLoading(false);
    };

    fetchResults();
  }, [location.search]);

  if (loading) return <div>Loading...</div>;
  if (error) return (
    <div>
      <div>Error: {error}</div>
      <button onClick={() => window.location.reload()}>Retry</button>
    </div>
  );

  return (
    <div className="search-results">
      <h1>Search Results</h1>
      {results.length > 0 ? (
        <ul>
          {results.map(doctor => (
            <li key={doctor.id} aria-label={`Doctor ${doctor.firstName} ${doctor.lastName}`}>
              <h2>{`${doctor.firstName} ${doctor.lastName}`}</h2>
              <p>Specialization ID: {doctor.specializationId}</p>
              <p>Region ID: {doctor.regionId}</p>
              <p>Contact: {doctor.contactNumber}</p>
              <p>Email: {doctor.email}</p>
            </li>
          ))}
        </ul>
      ) : (
        <p>No results found. Try adjusting your search criteria.</p>
      )}
    </div>
  );
};

export default SearchResultsComponent;