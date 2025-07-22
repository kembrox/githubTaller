using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;

namespace Flujo
{
    public class VehiculoFlujo : IVehiculoFlujo
    {
        private readonly IVehiculoDA _vehiculoDA;
        private readonly IRegistroReglas _registroReglas;
        private readonly IRevisionReglas _revisionReglas;
        public VehiculoFlujo(IVehiculoDA vehiculoDA, IRegistroReglas registroReglas, IRevisionReglas revisionReglas)
        {
            _vehiculoDA = vehiculoDA;
            _registroReglas = registroReglas;
            _revisionReglas = revisionReglas;
        }
        public async Task<IEnumerable<VehiculoResponse>> Obtener()
        {
            return await _vehiculoDA.Obtener();
        }
        public async Task<VehiculoDetalle> Obtener(Guid Id)
        {
            var vehiculo = await _vehiculoDA.Obtener(Id);
            vehiculo.RegistroValido = await _registroReglas.VehiculosEstaRegistrado
                (vehiculo.Placa, vehiculo.CorreoPropietario);
            vehiculo.RevisionValida = await _revisionReglas.RevisionEsValida
                (vehiculo.Placa);
            return vehiculo;
        }
        public async Task<Guid> Agregar(VehiculoRequest vehiculo)
        {
            return await _vehiculoDA.Agregar(vehiculo);
        }
        public async Task<Guid> Editar(Guid Id, VehiculoRequest vehiculo)
        {
            return await _vehiculoDA.Editar(Id, vehiculo);
        }
        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _vehiculoDA.Eliminar(Id);
        }
    }
}
