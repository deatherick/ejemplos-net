using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using AutoMapper;

namespace ErickExample
{
    public partial class ExportarExcel : Form
    {
        public ExportarExcel()
        {
            InitializeComponent();
            //var configuration = new MapperConfiguration(a => { a.AddProfile(new SimpleInvestorProfile()); });
            //var mapper = configuration.CreateMapper();
            var cobros = ObtenerDatos("PRUEBA_obtenercobros").Tables[0];
            //var result = mapper.Map<DataRowCollection, List<BitacoraCobro>>(cobros.Rows);
            dataGridView1.DataSource = cobros;

            textBox1.Parent = dataGridView1;              // nest the TextBox
            textBox2.Parent = dataGridView1;
            PlaceControl(dataGridView1, textBox1, 0);
            PlaceControl(dataGridView1, textBox2, 1);
        }

        private static void PlaceControl(DataGridView dgv, Control ctl, int index)
        {
            var r = dgv.GetColumnDisplayRectangle(index, true);  // or false
            ctl.Location = r.Location;
            ctl.Size = new Size(r.Width, dgv.ColumnHeadersHeight);
        }

        public DataSet ObtenerDatos(string procedimientoAlmacenado, List<SqlParameter> parametros = null)
        {
            var ds = new DataSet();
            using (var cn = new SqlConnection("Server=192.100.1.231; Initial Catalog=Tracker; Integrated Security=false; User Id=Desarrollo; Password=Caex2016.; MultipleActiveResultSets=True"))
            {
                var cmd = new SqlCommand(procedimientoAlmacenado, cn);
                using (cmd)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            //var cantidadTables = (ds.Tables.Count - 1);
            //var queryResult = ds.Tables[cantidadTables].Copy();
            //resultado.Tables.Add(queryResult);
            return ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var configuration = new MapperConfiguration(a => { a.AddProfile(new SimpleInvestorProfile()); });

                var mapper = configuration.CreateMapper();
                var cobros = ObtenerDatos("PRUEBA_obtenercobros").Tables[0];
                var result = mapper.Map<DataRowCollection, List<BitacoraCobro>>(cobros.Rows);
                foreach (var res in result)
                {
                    Console.WriteLine($@"ID: {res.IdCobro}");
                }
                MessageBox.Show($@"Se encontraron {result.Count} registros");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private void ExportarExcel_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            PlaceControl(dataGridView1, textBox1, 0);
            PlaceControl(dataGridView1, textBox2, 1);
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            PlaceControl(dataGridView1, textBox1, 0);
            PlaceControl(dataGridView1, textBox2, 1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = $"Evento LIKE '%{textBox2.Text}%'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = !textBox1.Visible;
            textBox2.Visible = !textBox2.Visible;
        }
    }

    public class BitacoraCobro
    {
        public int IdCobro { get; set; }
        public int IdGuiaMadre { get; set; }
        public int IdClienteEmpresa { get; set; }
        public double MontoaCobrar { get; set; }
    }

    public sealed class SimpleInvestorProfile : Profile
    {
        public SimpleInvestorProfile()
        {
            var mappingExpression = CreateMap<DataRow, BitacoraCobro>();
            mappingExpression.ForMember(d => d.IdCobro, o => o.MapFrom(s => s["IDCOBRO"]));
            mappingExpression.ForMember(d => d.IdGuiaMadre, o => o.MapFrom(s => s["IDGUIAMADRE"]));
            mappingExpression.ForMember(d => d.IdClienteEmpresa, o => o.MapFrom(s => s["IDCLIENTEEMPRESA"]));
            mappingExpression.ForMember(d => d.MontoaCobrar, o => o.MapFrom(s => s["MONTOACOBRAR"]));
        }
    }
}
