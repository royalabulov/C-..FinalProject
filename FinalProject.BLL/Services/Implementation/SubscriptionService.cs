using AutoMapper;
using FinalProject.BLL.Models.DTOs.SubscriptionDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using FinalProject.BLL.Services.Interface;
using FinalProject.Domain.Entites;
using FinalProject.Domain.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Implementation
{
	public class SubscriptionService : ISubscriptionService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public SubscriptionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

        public async Task<GenericResponseApi<bool>> CreateSubscription(CreateSubscriptionDTO createSubscription)
		{
			var response = new GenericResponseApi<bool>();

			if (createSubscription == null)
			{
				response.Failure("Vacancy data null", 404);
				return response;
			}
			var mapping = mapper.Map<Subscription>(createSubscription);
			await unitOfWork.GetRepository<Subscription>().AddAsync(mapping);
			await unitOfWork.Commit();

			return response;
		}

		public Task<GenericResponseApi<List<GetSubscriptionDTO>>> GetSubscriptions()
		{
			throw new NotImplementedException();
		}

		//get eliyende sub almis companylerde gelmelidi(mence)
		//public async Task<GenericResponseApi<List<GetSubscriptionDTO>>> GetSubscriptions()
		//{
		//	var response =  new GenericResponseApi<List<GetSubscriptionDTO>>();

		//	var subscriptionEntity = await unitOfWork.GetRepository<Subscription>().GetAsQueryable().Include(x=>x.).ToListAsync();

		//	if(subscriptionEntity == null)
		//	{
		//		response.Failure("subscriptionEntity not found", 404);
		//		return response;
		//	}
		//	var mapping = mapper.Map<List<GetSubscriptionDTO>>(subscriptionEntity);
		//	response.Success(mapping);

		//	return response;
		//}

		public async Task<GenericResponseApi<bool>> RemoveSubscription(int id)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await unitOfWork.GetRepository<Subscription>().GetById(id);
			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}
			unitOfWork.GetRepository<Subscription>().Remove(getById);
			await unitOfWork.Commit();
			return response;
		}

		public async Task<GenericResponseApi<bool>> UpdateSubscription(UpdateSubscriptionDTO updateSubscription)
		{
			var response = new GenericResponseApi<bool>();

			var getById = await unitOfWork.GetRepository<Subscription>().GetById(updateSubscription.Id);
			if (getById == null)
			{
				response.Failure("Id not found", 404);
				return response;
			}
			var mapping = mapper.Map(updateSubscription,getById);
			unitOfWork.GetRepository<Subscription>().Update(mapping);
			await unitOfWork.Commit();
			return response;
		}
	}
}
