namespace GSSServer.GSSDataSet.DataSetGSSTableAdapters
{
}
namespace GSSServer.GSSDataSet {
    
    
    public partial class DataSetGSS {
        partial class T_DepartmentDataTable
        {

        }
    
        partial class T_RolesDataTable
        {
        }
    
        partial class T_UsersDataTable
        {
            public override void EndInit()
            {
                base.EndInit();
                this.RowChanging += TestRowChangeEvent;

            }

            public void TestRowChangeEvent(object sender, System.Data.DataRowChangeEventArgs e)
            {

                //if ((short)e.Row[0].ToString().Length > 1)
                //{
                //    e.Row.SetColumnError("编号", "Quantity must be greater than 0");
                    
                //}
                //else
                //{
                //    e.Row.SetColumnError("编号", "");
                //}
            }
        }
    }
}
