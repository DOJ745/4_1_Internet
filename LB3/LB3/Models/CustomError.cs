using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LB3.Models
{
    public class CustomError
    {
		public int code;
		public ErrorLink _links;
		public CustomError(int code, string link)
		{
			this.code = code;
			this._links = new ErrorLink(link + "/api/error/" + code);
		}

		public class ErrorLink
		{
			public string details;
			public ErrorLink(string details) { this.details = details; }
		}
	}
}