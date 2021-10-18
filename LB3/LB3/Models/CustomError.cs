using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LB3.Models
{
    public class CustomError
    {
		public int code;
		public Link _links;
		public CustomError(int code, string link)
		{
			this.code = code;
			this._links = new Link(link + "/api/error/" + code);
		}

		public class Link
		{
			public string details;
			public Link(string details) { this.details = details; }
		}
	}
}