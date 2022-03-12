using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
	public class Cliente
	{
		private String c_Pessoa_numCC;
		private String numCliente;
		private String numTelemovel;
		private String nome;
		private String email;

		public String C_Pessoa_numCC
		{
			get { return c_Pessoa_numCC; }
			set
			{
				c_Pessoa_numCC = value;
			}
		}

		public String Nome
		{
			get { return nome; }
			set
			{
				nome = value;
			}
		}

		public String Email
		{
			get { return email; }
			set
			{
				email = value;
			}
		}

		public String NumCliente
		{
			get { return numCliente; }
			set
			{
				numCliente = value;
			}
		}

		public String NumTelemovel
		{
			get { return numTelemovel; }
			set
			{
				numTelemovel = value;
			}
		}

		public override String ToString()
		{
			return String.Format("{0,-20}{1,-20}{2,30}{3,30}            {4,30}", c_Pessoa_numCC, nome, numCliente, numTelemovel, email);
		}

		public Cliente() : base()
		{

		}

		public Cliente(String c_Pessoa_numC, String nome, String numCliente, String numTelemovel, String email) : base()
		{
			this.c_Pessoa_numCC = c_Pessoa_numC;
			this.numCliente = numCliente;
			this.numTelemovel = numTelemovel;
			this.nome = nome;
			this.email = email;
		}
	}
}

