using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
    public class Reserva
    {
        public static int codigo = 1;
        private String nomeCliente;
        private String numBilhete;
        private String data;
        private String numCruzeiro;
        private String clienteNumCC;

        public String ClienteNumCC
        {
            get { return clienteNumCC; }
            set
            {
                clienteNumCC = value;
            }
        }

        public String NumCruzeiro
        {
            get { return numCruzeiro; }
            set
            {
                numCruzeiro = value;
            }
        }

        public String DataCamp
        {
            get { return data; }
            set
            {
                data = value;
            }
        }

        public String NumBilhete
        {
            get { return numBilhete; }
            set
            {
                numBilhete = value;
                codigo = int.Parse(numBilhete);
            }
        }

        public String NomeCliente
        {
            get { return nomeCliente; }
            set
            {
                nomeCliente = value;
            }
        }

        public Reserva() : base()
        {

        }

        public Reserva(String numBilhete, String nomeCliente, String data, String numCruzeiro, String clienteNumCC) : base()
        {
            this.nomeCliente = nomeCliente;
            this.numBilhete = numBilhete;
            codigo = int.Parse(numBilhete);
            this.data = data;
            this.numCruzeiro = numCruzeiro;
            this.clienteNumCC = clienteNumCC;
        }

        public int nextCodigo()
        {
            return ++codigo;
        }

        public override String ToString()
        {
            return String.Format("{0,-40}{1,-40}{2,40}{3,40}            {4,40}", numBilhete, nomeCliente, data, numCruzeiro, clienteNumCC);
        }

    }
}
