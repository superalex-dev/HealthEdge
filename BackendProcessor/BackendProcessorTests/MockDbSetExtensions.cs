﻿using Microsoft.EntityFrameworkCore;
using Moq;

namespace BackendProcessorTests;

public static class MockDbSetExtensions
{
    public static Mock<DbSet<T>> AsDbSetMock<T>(this List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();

        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

        return dbSet;
    }
}