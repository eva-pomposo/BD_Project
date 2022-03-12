using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
    public class Barco
    {
		public static int codigo = 1;
		private String codigoBarco;
		private String numTotalBilhetes;
		private String nomeBarco;
		private String C_TipoBarco_nomeTipoBarco;

		public String CodigoBarco
		{
			get { return codigoBarco; }
			set {
				codigoBarco = value;
				codigo = int.Parse(codigoBarco);
			}
		}


		public String NumTotalBilhetes
		{
			get { return numTotalBilhetes; }
			set
			{
				numTotalBilhetes = value;
			}
		}

		public String NomeBarco
		{
			get { return nomeBarco; }
			set { nomeBarco = value; }
		}

		public String NomeTipoBarco
		{
			get { return C_TipoBarco_nomeTipoBarco; }
			set { C_TipoBarco_nomeTipoBarco = value; }
		}

		public override String ToString()
		{
			return String.Format("{0,-40}{1,-40}{2,40}{3,40}",codigoBarco,nomeBarco,C_TipoBarco_nomeTipoBarco, numTotalBilhetes);
		}

		public Barco() : base()
		{
		}

		public Barco(String codigoBarco, String numTotalBilhetes, String nomeBarco, String C_TipoBarco_nomeTipoBarco) : base()
		{
			this.codigoBarco = codigoBarco;
			codigo = int.Parse(codigoBarco);
			this.numTotalBilhetes = numTotalBilhetes;
			this.nomeBarco = nomeBarco;
			this.C_TipoBarco_nomeTipoBarco = C_TipoBarco_nomeTipoBarco;
		}

		public Barco(String codigoBarco) : base()
		{
			this.codigoBarco = codigoBarco;
			codigo = int.Parse(codigoBarco);
		}

		public int nextCodigo()
		{
			return ++codigo;
		}
	}
}
