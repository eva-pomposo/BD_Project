using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruzeirosDB
{
    class Cruzeiro
    {
        public static int codigo = 1;
        private String numCruzeiro;
        private String dataEmbarque;
        private String dataDesembarque;
        private String vagas;
        private String C_Barco_codigoBarco;
        private String horaPartida;
        private String horaChegada;
        private String localidadepartida;
        private String nomepartida;
        private String localidadeChegada;
        private String nomeChegada;
        private String idChegada;
        private String idPartida;

        public String IdChegada
        {
            get { return idChegada; }
            set
            {
                idChegada = value;
            }
        }

        public String IdPartida
        {
            get { return idPartida; }
            set
            {
                idPartida = value;
            }
        }

        public String NumCruzeiro
        {
            get { return numCruzeiro; }
            set
            {
                numCruzeiro = value;
                codigo = int.Parse(numCruzeiro);
            }
        }

        public String Nomepartida
        {
            get { return nomepartida; }
            set
            {
                nomepartida = value;
            }
        }

        public String NomeChegada
        {
            get { return nomeChegada; }
            set
            {
                nomeChegada = value;
            }
        }

        public String HoraPartida
        {
            get { return horaPartida; }
            set
            {
                horaPartida = value;
            }
        }

        public String HoraChegada
        {
            get { return horaChegada; }
            set
            {
                horaChegada = value;
            }
        }

        public String Localidadepartida
        {
            get { return localidadepartida; }
            set
            {
                localidadepartida = value;
            }
        }

        public String LocalidadeChegada
        {
            get { return localidadeChegada; }
            set
            {
                localidadeChegada = value;
            }
        }

        public String DataEmbarque
        {
            get { return dataEmbarque; }
            set
            {
                dataEmbarque = value;
            }
        }

        public String DataDesembarque
        {
            get { return dataDesembarque; }
            set
            {
                dataDesembarque = value;
            }
        }

        public String Vagas
        {
            get { return vagas; }
            set
            {
                vagas = value;
            }
        }

        public String codigoBarco
        {
            get { return C_Barco_codigoBarco; }
            set
            {
                C_Barco_codigoBarco = value;
            }
        }

        public override String ToString()
        {
            String q = "{0,-" + (30 - numCruzeiro.Length) + "}";
            q += "{1,-" + (30 - C_Barco_codigoBarco.Length) + "}";
            q += "{2,-" + (35 - dataEmbarque.Length) + "}";
            q += "{3,-" + (60 - localidadepartida.Length) + "}";
            q += "{4,-" + (40 - horaPartida.Length) + "}";
            q += "{5,-" + (50 - nomepartida.Length) + "}";
            q += "{6," + (15 - dataDesembarque.Length) + "}";
            q += "{7," + (70 - localidadeChegada.Length) + "}";
            q += "{8," + (50 - horaChegada.Length) + "}";
            q += "{9," + (50 - nomeChegada.Length) + "}";



            return String.Format(q,numCruzeiro ,C_Barco_codigoBarco, dataEmbarque,  localidadepartida, horaPartida, nomepartida, dataDesembarque, localidadeChegada, horaChegada, nomeChegada);
            
        }

        public Cruzeiro() : base()
        {
        }

        public Cruzeiro(String C_Barco_codigoBarco, String numCruzeiro, String dataEmbarque, String dataDesembarque, String vagas) : base()
        {
            this.C_Barco_codigoBarco = C_Barco_codigoBarco;
            codigo = int.Parse(C_Barco_codigoBarco);
            this.numCruzeiro = numCruzeiro;
            this.dataEmbarque = dataEmbarque;
            this.dataDesembarque = dataDesembarque;
            this.vagas = vagas;

        }

        public Cruzeiro(String C_Barco_codigoBarco, String numCruzeiro, String dataEmbarque, String dataDesembarque) : base()
        {
            this.C_Barco_codigoBarco = C_Barco_codigoBarco;
            codigo = int.Parse(C_Barco_codigoBarco);
            this.numCruzeiro = numCruzeiro;
            this.dataEmbarque = dataEmbarque;
            this.dataDesembarque = dataDesembarque;
        }

        public int nextCodigo()
        {
            return ++codigo;
        }

        public Cruzeiro(String numCruzeiro,String C_Barco_codigoBarco, String dataEmbarque, String localidadepartida, String horaPartida, String nomepartida, String dataDesembarque, String localidadeChegada, String horaChegada, String nomeChegada)
        {
            this.C_Barco_codigoBarco = C_Barco_codigoBarco;
            codigo = int.Parse(C_Barco_codigoBarco);
            this.numCruzeiro = numCruzeiro;
            this.dataEmbarque = dataEmbarque;
            this.dataDesembarque = dataDesembarque;
            this.localidadepartida = localidadepartida;
            this.horaPartida = horaPartida;
            this.nomepartida = nomepartida;
            this.localidadeChegada = localidadeChegada;
            this.horaChegada = horaChegada;
            this.nomeChegada =  nomeChegada;
        }

    }
}

