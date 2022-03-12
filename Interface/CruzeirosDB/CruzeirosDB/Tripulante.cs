using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
	public class Tripulante
	{
		private String c_Pessoa_numCC;
		private String numTripulante;
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

		public String NumTripulante
		{
			get { return numTripulante; }
			set
			{
				numTripulante = value;
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
			return String.Format("{0,-20}{1,-20}{2,30}{3,40}           {4,50}", c_Pessoa_numCC, nome, numTripulante, numTelemovel, email);
		}

		public Tripulante() : base()
		{

		}

		public Tripulante(String c_Pessoa_numC, String numTripulante, String numTelemovel, String nome, String email) : base()
		{
			this.c_Pessoa_numCC = c_Pessoa_numC;
			this.numTripulante = numTripulante;
			this.numTelemovel = numTelemovel;
			this.nome = nome;
			this.email = email;
		}
	}



}
