import React, { useEffect, useState } from 'react';
import { StarIcon } from '@heroicons/react/solid';

const DoctorCard = ({ doctor }) => {
  const [earliestSlot, setEarliestSlot] = useState(null);

  useEffect(() => {
    const url = `http://localhost:5239/availableSlots/${doctor.id}?date=${new Date().toISOString()}`;

    fetch(url)
      .then(response => response.json())
      .then(data => {
        setEarliestSlot(data);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }, [doctor.id]);

  return (
    <div className="bg-white shadow-lg rounded-lg p-6 flex flex-col items-start">
      <img className="w-24 h-24 rounded-full mb-4" src={doctor.photoUrl} alt={doctor.name} />
      <h3 className="text-lg font-semibold">{doctor.name}</h3>
      <p className="text-sm text-gray-600">{doctor.specialization}</p>
      <div className="flex items-center my-2">
        {[...Array(5)].map((_, i) => (
          <StarIcon key={i} className="h-5 w-5 text-yellow-400" />
        ))}
        <span className="text-sm text-gray-600 ml-2">{`${doctor.ratings} оценки`}</span>
      </div>
      <p className="text-sm text-gray-600">{`${doctor.city}`}</p>
      <p className="text-sm text-gray-500 mb-4">{`Най-ранен час: ${earliestSlot}`}</p>
      <button className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">
        Запази час
      </button>
    </div>
  );
};

export default DoctorCard;