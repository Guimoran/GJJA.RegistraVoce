using System.Collections.Generic;
using GJJA.RegistraVoce.Domain;
using GJJA.RegistraVoce.DataAcess.Utils;
using System.Data.Common;
using System;
using GJJA.RegistraVoce.Domain.Enums;
using System.Data;
using GJJA.RegistraVoce.DataAcess.Extensions;

namespace GJJA.RegistraVoce.DataAcess.DAOs
{
    public class PersonDAO
    {
        private string _connectionString;
        public PersonDAO(String connectionString)
        {
            _connectionString = connectionString;
                        
        }
        // METODO DE SELECT

        // try finally 

        public List<Person> Select()
        {
            // Aplicando o  acoplamento   principio de liskov
            // DbUtils dbUtils = new DbUtils();

            List<Person> people = new List<Person>();
            // faz a parte de conexão com banco para fechar assim evitar usar try cath
            using (DbConnection conn = DbUtils.CreateConnection(_connectionString))
            using (DbCommand command = conn.CreateCommand())
            {

                //  DbConnection conn = null;
                // exclui o try pois fecha conexão de outra forma.
                //  try
                //  {
                //  conn = DbUtils.CreateConnection();
                //  DbCommand command = conn.CreateCommand();  //CRIO COMANDO 
                command.CommandText = "SELECT * FROM pes_pessoas";
                command.CommandType = System.Data.CommandType.Text;
                DbDataReader reader = command.ExecuteReader(); //LEIO RESULTADO DE COMANDO
                while (reader.Read())
                {
                    people.Add(ExtractFromDataReader(reader));
                }
                return people;
            }

            //   }

            //   finally
            //    {
            //        DbUtils.CloseConnection(conn);
            //   }
        }

        public Person SelectById(int personId)
        {
            using (DbConnection conn = DbUtils.CreateConnection(_connectionString))
            using (DbCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM pes_pessoas WHERE pes_id = @pes_id";
                command.SetParameter("@pes_id", personId);
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ExtractFromDataReader(reader);
                }
                return null;
            }
        }



        // INSERT
        public void Insert(Person person)
        {
            using (DbConnection conn = DbUtils.CreateConnection(_connectionString))
            using (DbCommand command = conn.CreateCommand())
            {
                command.CommandText = "INSERT INTO pes_pessoas(pes_nome, pes_sexo, pes_cpf, pes_rg, pes_data_nascimento, pes_estado_civil, pes_endereco, pes_telefone)" +
                                      "VALUES (@pes_nome, @pes_sexo, @pes_cpf, @pes_rg, @pes_data_nascimento, @pes_estado_civil, @pes_endereco, @pes_telefone)";
                SetParameters(person, command);
                command.ExecuteNonQuery();



                /* não precisa mais pois foi feito metodo para melhorar essa implementação                                       
                  DbParameter nameParameter = command.CreateParameter();
                  nameParameter.ParameterName = "@pes_name";
                  nameParameter.Value = person.Name;
                  command.Parameters.Add(nameParameter);
                                    */
                /*  --Não sera mais necessário utilizar
                  command.SetParameter("@pes_nome", person.Name);    
                  command.SetParameter("@pes_sexo", person.Gender);                
                  command.SetParameter("@pes_cpf", person.DocumentNumber);
                  command.SetParameter("@pes_rg", person.Identification);
                  command.SetParameter("@pes_data_nascimento", person.BirthDate);
                  command.SetParameter("@pes_estado_civil", person.MaritalStatus);
                  command.SetParameter("@pes_endereco", person.Address);
                  command.SetParameter("@pes_telefone", person.Phone);
                  command.ExecuteNonQuery(); 
                */


            }
        }

        //UPDATE
        public void Update(Person person)
        {
            using (DbConnection connection = DbUtils.CreateConnection(_connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE pes_pessoas SET " +
                                      "pes_nome = @pes_nome, " +
                                      "pes_sexo = @pes_sexo, " +
                                      "pes_cpf = @pes_cpf, " +
                                      "pes_rg = @pes_rg, " +
                                      "pes_data_nascimento = @pes_data_nascimento, " +
                                      "pes_estado_civil = @pes_estado_civil, " +
                                      "pes_endereco = @pes_endereco, " +
                                      "pes_telefone = @pes_telefone " +
                                      "WHERE pes_id = @pes_id";

                SetParameters(person, command);
                command.SetParameter("@pes_id", person.Id);
                command.ExecuteNonQuery();

            }

        }



        //DELETE

        public void Delete (int personId)
        {
           using (DbConnection connection = DbUtils.CreateConnection(_connectionString))
           using (DbCommand command = connection.CreateCommand())
           {
               command.CommandText = "DELETE FROM pes_pessoas WHERE pes_id = @pes_id";
               command.SetParameter ("@pes_id", personId);
               command.ExecuteNonQuery();
            
           }


        } 
        public int Count()
        {
            using (DbConnection connection = DbUtils.CreateConnection(_connectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(pes_id) FROM pes_pessoas";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }




        private void SetParameters(Person person, DbCommand command)
        {
            command.SetParameter("@pes_nome", person.Name);
            command.SetParameter("@pes_sexo", person.Gender);
            command.SetParameter("@pes_cpf", person.DocumentNumber);
            command.SetParameter("@pes_rg", person.Identification);
            command.SetParameter("@pes_data_nascimento", person.BirthDate);
            command.SetParameter("@pes_estado_civil", person.MaritalStatus);
            command.SetParameter("@pes_endereco", person.Address);
            command.SetParameter("@pes_telefone", person.Phone);
        }
        private Person ExtractFromDataReader(DbDataReader reader)
        {
            return new Person
            {
                Id = Convert.ToInt32(reader["pes_id"]),
                Name = reader["pes_nome"].ToString(),
                Gender = (Gender)Convert.ToInt32(reader["pes_sexo"]),
                DocumentNumber = reader["pes_cpf"].ToString(),
                Identification = reader["pes_rg"].ToString(),
                BirthDate = Convert.ToDateTime(reader["pes_data_nascimento"]),
                MaritalStatus = (MaritalStatus)Convert.ToInt32(reader["pes_estado_civil"]),
                Address = reader["pes_endereco"].ToString(),
                Phone = reader["pes_telefone"].ToString(),

            };
        }
    }


}