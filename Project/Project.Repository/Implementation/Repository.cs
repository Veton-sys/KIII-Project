﻿using Project.Domain.DomainModels;
using Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project.Repository.Implementation
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext context;
		private DbSet<T> entities;
		string errorMessage = string.Empty;
		
		public Repository(ApplicationDbContext context)
		{
			this.context = context;
			entities = context.Set<T>();
		}
		public T Get(Guid? id)
		{
			return entities.SingleOrDefault(s => s.Id == id);
		}

		public IEnumerable<T> GetAll()
		{
			return entities.AsEnumerable();
		}

		public void Insert(T entity)
		{
			if(entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Add(entity);
			context.SaveChanges();
		}

		public void Update(T entity)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Update(entity);
			context.SaveChanges();
		}
		public void Delete(T entity)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Remove(entity);
			context.SaveChanges();
		}

	}
}
