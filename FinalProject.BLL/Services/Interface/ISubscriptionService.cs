using FinalProject.BLL.Models.DTOs.SubscriptionDTOs;
using FinalProject.BLL.Models.Exception.GenericResponseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Services.Interface
{
	public interface ISubscriptionService
	{
		Task<GenericResponseApi<List<GetSubscriptionDTO>>> GetSubscriptions();
		Task<GenericResponseApi<bool>> CreateSubscription(CreateSubscriptionDTO createSubscription);
		Task<GenericResponseApi<bool>> UpdateSubscription(UpdateSubscriptionDTO updateSubscription);
		Task<GenericResponseApi<bool>> RemoveSubscription(int id);
	}
}
