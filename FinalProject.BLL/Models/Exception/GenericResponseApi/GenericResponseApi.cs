using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Exception.GenericResponseApi
{
	public class GenericResponseApi<T>
	{
		public T Data { get; set; }
		public bool IsSuccess {  get; set; }
		public string ErrorMesssage {  get; set; }
		public List<string> Errors { get; set; }
		public int StatusCode {  get; set; }

		public GenericResponseApi()
		{
			Errors = new List<string>();
		}

		public GenericResponseApi(T data) : this()
		{
			Data = data;
			IsSuccess = true;
			StatusCode = 200;
		}


		public GenericResponseApi(List<string> errors) : this()
		{
			Errors = errors;
			IsSuccess = false;
			StatusCode = 400;
		}


		public void Success(T data, int StatusCode = 200)
		{
			IsSuccess = true;
			Errors = null;
			Data = data;
			this.StatusCode = StatusCode;

		}

		public void Failure(List<string> errors, int StatusCode = 500)
		{
			Data = default;
			Errors = errors;
			this.StatusCode = StatusCode;
			IsSuccess = false;
		}

		public void Failure(string error, int StatusCode = 500)
		{
			Data = default;
			Errors = new List<string> { error };
			this.StatusCode = StatusCode;
			IsSuccess = false;
		}

	}
}
