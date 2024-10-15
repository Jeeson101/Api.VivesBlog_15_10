namespace Vives.Services.Model.Extensions
{
    public static class ServiceResultExtensions
    {
        public static TServiceResult NotFound<TServiceResult>(this TServiceResult serviceResult, string entityName, ServiceMessageType type = ServiceMessageType.Warning)
        where TServiceResult : ServiceResult
        {
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "NotFound",
                Message = $"Could not find {entityName}",
                Type = type
            });

            return serviceResult;
        }

        public static void LoginFailed(this ServiceResult serviceResult)
        {
	        serviceResult.Messages.Add(new ServiceMessage
	        {
		        Code = "LoginFailed",
		        Message = "The login failed.",
		        Type = ServiceMessageType.Info
	        });
        }
	}
}
