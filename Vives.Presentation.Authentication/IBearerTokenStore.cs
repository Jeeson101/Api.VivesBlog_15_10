using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Presentation.Authentication
{
	public interface IBearerTokenStore
	{
		public string GetToken();
		public void SetToken(string token);
	}
}
