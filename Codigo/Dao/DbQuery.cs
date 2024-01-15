using AnnarComMICROSESV60.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AnnarComMICROSESV60.Dao.ConnectionDB;

namespace AnnarComMICROSESV60.Dao
{
    public class DbQuery
    {
        public ConnectionDB conexion = new ConnectionDB();


        public ResultadoQuery ConsultaPaciente(string tubo)
        {
            string sqlConsultaPaciente = $"select pe.nit, pe.consecutivo_especie, pe.tipo_raza, pe.tipo_especie, p.sede_codigo, p.si_band04 from paciente p right JOIN propietario_especie pe on pe.nit = p.nit and pe.consecutivo_especie = p.consecutivo_especie where pe.tipodcto_cod = p.tipodcto_cod and paciente_cod = '{tubo}' and fecha >= current_date - {InterfaceConfig.diasatras}";

            return conexion.RunQuery(sqlConsultaPaciente,  CommandType.Text);

        }

        public ResultadoQuery ConsultaTodosPacientes(string tubo, string variable)
        {
            string ConsultaTodosPacientes = $"select DISTINCT * from paciente_examenes pe inner join homologacion h on h.examen_cod = pe.examen_cod and h.equipo_cod = '{InterfaceConfig.nombreEquipo}' and h.analito > '' where paciente_cod = '{tubo.Trim()}' and pe.examen_cod = h.examen_cod and fecha >= current_date - {InterfaceConfig.diasatras} and h.examen_cod_equipo = '{variable}'";

            return conexion.RunQuery(ConsultaTodosPacientes, CommandType.Text);
        }

        public ResultadoQuery ConsultaHomologacion(string tubo, string examenCodEquipo, string[] arrNroPac, string strSeccion, string strTapa)
        {
            string sqlConsultaHomologacion = $"Select h.examen_cod,h.analito, h.decimales, p.sede_codigo from examenes e " +
                $"Join grupos_examenes g on e.gruexa_cod = g.gruexa_cod Join homologacion h on h.examen_cod = e.examen_cod" +
                $" And h.equipo_cod= '{InterfaceConfig.nombreEquipo}' and h.analito > '' " +
                $"join paciente_examenes p on p.examen_cod = h.examen_cod Where h.examen_cod_equipo = '{examenCodEquipo.Trim()}' " +
                $"and fecha >= current_date - {InterfaceConfig.diasatras} and p.paciente_cod = '{tubo}'";

            var strsentencia = sqlConsultaHomologacion;

            if (arrNroPac.Length > 1)//de momento no entra al if, entonces aun no se valida si funciona correctamente
            {
                if (string.IsNullOrEmpty(strSeccion))
                {
                    strsentencia = strsentencia + " and (g.ccto_cg1 is null or trim(g.ccto_cg1) = '')";
                }
                else
                {
                    strsentencia = strsentencia + " and g.ccto_cg1='" + strSeccion + "'";
                }

                if (string.IsNullOrEmpty(strTapa))
                {
                    strsentencia = strsentencia + " and (e.tipoenv_cod is null or trim(e.tipoenv_cod) = '')";
                }
                else
                {
                    strsentencia = strsentencia + " and e.tipoenv_cod='" + strTapa + "'";
                }
            }
            else
            {
                strsentencia = sqlConsultaHomologacion;
            }

            return conexion.RunQuery(strsentencia, CommandType.Text);
        }

        public ResultadoQuery ConsultaResulLab(string tubo, string examen)
        {
            string sqlConsultaResulLab = $"Select count(*) FROM resul_lab WHERE paciente_cod = '{tubo.Trim()}' and examen_cod= '{examen.Trim()}' and fecha >= current_date - {InterfaceConfig.diasatras}";

            return conexion.RunQuery(sqlConsultaResulLab, CommandType.Text);
        }

        public ResultadoStatement InsertCaratulasEquipos(string tubo, string examen, string raza, string especie)
        {
            string sqlInsertResulCE = $@"INSERT INTO resul_lab(paciente_cod, fecha, sede_codigo, tipodcto_cod, nit, consecutivo_especie,
                                                 examen_cod, reg_exa, analito_cod, secuencia, codigo, resultado,
                                                 analito, minimo, intermedio, maximo, unidades, equipo, reactivo,
                                                 tablav, tablaa, tabla1, tabla2, activo)
                                                 select paciente_examenes.paciente_cod, p.fecha_grabo,paciente_examenes.sede_codigo, p.tipodcto_cod, p.nit,
                                                 p.consecutivo_especie, paciente_examenes.examen_cod, paciente_examenes.reg_exa, caratulas_equipos.codili, '1', 1, ' ',
                                                 caratulas_equipos.nombre, caratulas_equipos.minimo,caratulas_equipos.intermedio,caratulas_equipos.maximo,
                                                 caratulas_equipos.unid, '{InterfaceConfig.equipoCodigoCaratula}','{InterfaceConfig.recativoCodigoCaratula}', caratulas_equipos.tablav,
                                                 caratulas_equipos.tablaa, caratulas_equipos.tabla1, caratulas_equipos.tabla2, caratulas_equipos.activo
                                                 from paciente_examenes
                                                 join caratulas_equipos on caratulas_equipos.examen_cod = paciente_examenes.examen_cod
                                                 join paciente p on p.paciente_cod = paciente_examenes.paciente_cod
                                                 join propietario_especie pp on pp.nit = p.nit and pp.consecutivo_especie = p.consecutivo_especie
                                                 where paciente_examenes.fecha >= current_date - {InterfaceConfig.diasatras}
                                                 and paciente_examenes.paciente_cod = '{tubo}'
                                                 and paciente_examenes.examen_cod = '{examen}'
                                                 and caratulas_equipos.raza_cod = '{raza}'
                                                 and caratulas_equipos.especie_cod = '{especie}'
                                                 and caratulas_equipos.equipo_cod = '{InterfaceConfig.equipoCodigoCaratula}'
                                                 and caratulas_equipos.reactivo_cod = '{InterfaceConfig.recativoCodigoCaratula}'";

            return conexion.RunStatement(sqlInsertResulCE, null, CommandType.Text);
        }

        public ResultadoStatement InsertCaratulasBasica(string tubo, string examen)
        {
            string sqlInsertResulCB = $@"INSERT INTO resul_lab(paciente_cod, fecha, sede_codigo, tipodcto_cod, nit, consecutivo_especie,
                                                 examen_cod, reg_exa, analito_cod, secuencia, codigo, resultado,
                                                 analito, minimo, intermedio, maximo, unidades, equipo, reactivo,
                                                 tablav, tablaa, tabla1, tabla2, activo)
                                                 select paciente_examenes.paciente_cod, p.fecha_grabo,paciente_examenes.sede_codigo, p.tipodcto_cod, p.nit,
                                                 p.consecutivo_especie, paciente_examenes.examen_cod, paciente_examenes.reg_exa, caratula_basica.analito_cod, '1', 1, ' ',
                                                 caratula_basica.nombre, caratula_basica.minimo,caratula_basica.intermedio,caratula_basica.maximo,
                                                 caratula_basica.unid, '{InterfaceConfig.equipoCodigoCaratula}','{InterfaceConfig.recativoCodigoCaratula}', caratula_basica.tablav,
                                                 caratula_basica.tablaa, caratula_basica.tabla1, caratula_basica.tabla2, caratula_basica.activo
                                                 from paciente_examenes
                                                 join caratula_basica on caratula_basica.examen_cod = paciente_examenes.examen_cod
                                                 join paciente p on p.paciente_cod = paciente_examenes.paciente_cod
                                                 where paciente_examenes.fecha >= current_date - {InterfaceConfig.diasatras}
                                                 and paciente_examenes.paciente_cod = '{tubo}'
                                                 and paciente_examenes.examen_cod = '{examen}'
                                                 and caratula_basica.equipo_cod = '{InterfaceConfig.equipoCodigoCaratula}'
                                                 and caratula_basica.reactivo_cod = '{InterfaceConfig.recativoCodigoCaratula}'";

            return conexion.RunStatement(sqlInsertResulCB, null, CommandType.Text);
        }

        public ResultadoStatement UpdatePacienteExamenes(string paciente, string examen, string vfuncional)
        {
            string sqlUpdateResult;

            if (InterfaceConfig.ValidaResultado == "S")
            {
                if (vfuncional == "N")
                {
                    sqlUpdateResult = $@"update paciente_examenes set contestado=true,validado= true, respondido_por='INTERFAZ',validado_por='INTERFAZ', fec_val=current_date 
                                     where paciente_cod= '{paciente}' and examen_cod= '{examen}' and fecha >= current_date - {InterfaceConfig.diasatras}";
                }
                else
                {

                    sqlUpdateResult = $@"select count(*) from resul_lab r
                                     right outer join  caratula_basica c on  r.examen_cod= c.examen_cod and r.analito_cod = c.analito_cod
                                     where paciente_cod = '{paciente}'  and r.examen_cod= '{examen}' and  resequi like'%R%' and (resultado = '' or resultado is null) and r.fecha >= current_date - {InterfaceConfig.diasatras}";


                    int intCantidadRegistros = int.Parse(sqlUpdateResult);

                    if (intCantidadRegistros == 0)
                    {
                        sqlUpdateResult = $@"update paciente_examenes set contestado=true,validado= true, respondido_por='INTERFAZ',validado_por='INTERFAZ', fec_val=current_date 
                                     where paciente_cod= '{paciente}' and examen_cod= '{examen}' and fecha >= current_date - {InterfaceConfig.diasatras}";
                    }
                    else
                    {
                        sqlUpdateResult = $@"update paciente_examenes set contestado=true,modificado= true, respondido_por='INTERFAZ', fec_mod=current_date 
                                     where paciente_cod= '{paciente}' and examen_cod= '{examen}' and fecha >= current_date - {InterfaceConfig.diasatras}";
                    }

                }
            }
            else
            {
                sqlUpdateResult = $@"update paciente_examenes set contestado=true,modificado= true, respondido_por='INTERFAZ', fec_mod=current_date 
                                     where paciente_cod= '{paciente}' and examen_cod= '{examen}' and fecha >= current_date - {InterfaceConfig.diasatras}";
            }

            return conexion.RunStatement(sqlUpdateResult, null, CommandType.Text);
        }

        public ResultadoQuery ConsultaResulPrevios(string paciente, string examen)
        {
            string sqlConsultaResulPrevios = $"Select count(*) FROM paciente_examenes WHERE paciente_cod = '{paciente.Trim()}' and examen_cod= '{examen.Trim()}' and validado= true and fecha >= current_date - {InterfaceConfig.diasatras}";

            return conexion.RunQuery(sqlConsultaResulPrevios, CommandType.Text);
        }

        public ResultadoQuery GuardaResultlab(string resultado, string paciente, string examen, string analito)
        {
            string sqlConsultaResulPrevios = $"UPDATE resul_lab SET resultado = '{resultado}' WHERE paciente_cod = '{paciente}' and fecha >= current_date - {InterfaceConfig.diasatras} and examen_cod= '{examen}' and analito_cod= '{analito}'";

            return conexion.RunQuery(sqlConsultaResulPrevios, CommandType.Text);
        }

        public ResultadoQuery RegistraEventoResultado(string paciente, string examen, string sSql)
        {
            string Consulta;

            if (sSql == "S")
            {
                Consulta = $"select reg_exa,fecha,hora_recepcion from paciente_examenes where paciente_cod = '{paciente.Trim()}' and fecha >= current_date - {InterfaceConfig.diasatras} and examen_cod = '{examen}'";  
            }
            else
            {
                Consulta = "";
            }

            return conexion.RunQuery(Consulta, CommandType.Text);
        }

        public ResultadoStatement DeleteEInsertEventoResultado(string paciente, string examen, string strHora, string varFecha, string cod_sede, string regexa, string dtFecha, string strComandoBorraPacienteEventoExamen, string strComandoCreacionPacienteEventoExamen)
        {
            string resultAccion;

            if (strComandoBorraPacienteEventoExamen == "S")
            {
                resultAccion = $@"delete from eventos_paciente_exam where paciente_cod = '{paciente.Trim()}' and fecha >= current_date - {InterfaceConfig.diasatras} and tipo_even_cod = '020' and examen_cod = '{examen}'";
            }
            else if (strComandoCreacionPacienteEventoExamen == "S")
            {
                resultAccion = $@"INSERT INTO eventos_paciente_exam(paciente_cod, hora_event, fecha, sede_codigo, examen_cod, reg_exa, tipo_even_cod, fecha_event, observ_event, usr_codigo, activo, secuencia) VALUES('{paciente.Trim()}', '{strHora}', '{varFecha}', '{cod_sede}','{examen}', {regexa} , '020', '{dtFecha}', 'ADICIONO RESULTADO POR {InterfaceConfig.nombreEquipo}', 'INTERFAZ', true, 1)";
            }
            else
            {
                resultAccion = "";
            }

            return conexion.RunStatement(resultAccion, null, CommandType.Text);
        }

        public ResultadoStatement UpdatePaquete(string vCodPaciente, string strExamen, string strregExa) //queda listo para implementarse en enviapaquete()
        {
            string UpdatePaquete = $@"update paciente_examenes set enviado_interfaz= 'S' where paciente_cod = '0' and examen in(1) and reg_exa  in(2), {vCodPaciente}, {strExamen}, {strregExa}";

            return conexion.RunStatement(UpdatePaquete, null, CommandType.Text);
        }

        public ResultadoQuery buscarOrdenes(string muestra, string strSql, string strQuery)
        {
            string ConsultaOrden;

            if (strSql == "")
            {
                ConsultaOrden = $@"select distinct pa.paciente_cod,pa.tipodcto_cod,pa.nit,pa.nacio, pa.nom1, pa.ape1,pa.sexo,pa.edad
                                                        from paciente pa
                                                        join paciente_examenes pe on pa.paciente_cod = pe.paciente_cod  and
                                                        pa.fecha = pe.fecha and pa.hora = pe.hora
                                                        join examenes ex on ex.examen_cod = pe.examen
                                                        join homologacion ho on ho.equipo_cod= '{InterfaceConfig.nombreEquipo}'  AND ho.examen_cod = pe.examen
                                                        where pe.fecha >= current_date - InterfaceConfig.diasatras 
                                                        and  pe.paciente_cod= '{muestra}' and pa.fecha >= current_date - {InterfaceConfig.diasatras} ";

                ConsultaOrden = ConsultaOrden + @" group by pa.paciente_cod,pa.tipodcto_cod,pa.nit,pa.nacio, pa.nom1, pa.ape1,pa.sexo,pa.edad order by pa.paciente_cod  limit 1 ";
            }
            else if (strQuery == "")
            {
                ConsultaOrden = $@" Select paciente_cod, ex.gruexa_cod,
                                                                pe.examen examen_annar,
                                                                ho.examen_cod_equipo examen_cent,
                                                               (pe.fecha+pe.hora) fecha,
                                                                pe.reg_exa reg_exa,
                                                                ex.tub_ind,
                                                                ex.prefijo
                                                                From paciente_examenes  pe
                                                                Left Join  perfil_etiquetas pt  on pt.perfil_cod = pe.examen  
                                                                Join homologacion ho on ho.equipo_cod= '{InterfaceConfig.nombreEquipo}' and ho.examen_cod = pe.examen and (ho.analito is null or trim(ho.analito) = '')
                                                                Join examenes ex on ex.examen_cod =  
                                                                Case WHEN pt.examen_cod Is null then examen else pt.examen_cod end ";

                ConsultaOrden = ConsultaOrden + $" WHERE pe.fecha >= current_date - {InterfaceConfig.diasatras} And pe.paciente_cod = '{muestra}'";
            }
            else
            {
                ConsultaOrden = "";
            }

            return conexion.RunQuery(ConsultaOrden, CommandType.Text);
        }

        public ResultadoQuery HomologacionHistogramas(string analito, string paciente)
        {
            //string sqlConsultaHomologacion = $"Select h.examen_cod,h.analito, h.decimales, p.sede_codigo from examenes e Join grupos_examenes g on e.gruexa_cod = g.gruexa_cod Join homologacion h on h.examen_cod = e.examen_cod And h.equipo_cod= '{InterfaceConfig.nombreEquipo}' and h.analito > '' join paciente_examenes p on p.examen_cod = h.examen_cod Where h.examen_cod_equipo = '{examenCodEquipo.Trim()}' and fecha >= current_date - {InterfaceConfig.diasatras} and p.paciente_cod = '{paciente}'";

            string sqlConsultaHomologacion = $"Select * From HOMOLOGACION h join paciente_examenes pe on h.examen_cod = pe.examen_cod WHERE h.equipo_cod = '{InterfaceConfig.nombreEquipo}'  and h.EXAMEN_COD_EQUIPO='{analito}' and   pe.paciente_cod = '{paciente}' and pe.fecha >= current_date - {InterfaceConfig.diasatras}";

            return conexion.RunQuery(sqlConsultaHomologacion, CommandType.Text);
        }

        public ResultadoStatement ActualizarGrafica(string p_imagenN, string codPaciente, string vEstudio, List<NpgsqlParameter> imagen)
        {
            string query = $@"UPDATE paciente_examenes 
                                    SET {p_imagenN} = @imagen 
                                    WHERE paciente_cod= '{codPaciente}' 
                                    and examen_cod= ''";

            return conexion.RunStatement(query,imagen, CommandType.Text);

        }
    }
}
