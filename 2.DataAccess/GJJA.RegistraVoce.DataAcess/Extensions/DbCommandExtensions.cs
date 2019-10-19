using System.Data.Common;

namespace GJJA.RegistraVoce.DataAcess.Extensions
{
    public static class DbCommandExtensions
    // todo metodo de extensions e public e static
    // o primeiro parametro indica  a classe que ira conectar no metodo (this)
    
    {
        public static void SetParameter <TParameterType> (this DbCommand command, string parameterName, TParameterType parameterValue)
       {
           DbParameter parameter = command.CreateParameter();
           parameter.ParameterName = parameterName;
           parameter.Value = parameterValue;
           command.Parameters.Add(parameter);
       }


    }



}