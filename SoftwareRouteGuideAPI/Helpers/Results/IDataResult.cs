namespace SoftwareRouteGuideAPI.Helpers.Results
{
    public interface IDataResult<T>:IResult
    {
         T Data { get; }
    }
}