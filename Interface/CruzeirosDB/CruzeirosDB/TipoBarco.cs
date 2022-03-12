using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
    public class TipoBarco
    {
		private String nomeTipoBarco;
		private String maxBilhetes;
		private String classificacao;

		public String NomeTipoBarco
		{
			get { return nomeTipoBarco; }
			set
			{
				nomeTipoBarco = value;
			}
		}


		public String MaxBilhetes
		{
			get { return maxBilhetes; }
			set
			{
				maxBilhetes = value;
			}
		}

		public String Classificacao
		{
			get { return classificacao; }
			set { classificacao = value; }
		}

		public override String ToString()
		{
			return nomeTipoBarco;
		}

		public TipoBarco()
		{
		}

		public TipoBarco(String nomeTipoBarco, String maxBilhetes, String classificacao) : base()
		{
			this.nomeTipoBarco = nomeTipoBarco;
			this.maxBilhetes = maxBilhetes;
			this.classificacao = classificacao;
		}

		public TipoBarco(String nomeTipoBarco) : base()
		{
			this.nomeTipoBarco = nomeTipoBarco;
		}

	}
}
