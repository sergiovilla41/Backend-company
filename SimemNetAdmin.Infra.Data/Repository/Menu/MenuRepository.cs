using Microsoft.EntityFrameworkCore;
using SimemNetAdmin.Domain.Interfaces;
using SimemNetAdmin.Domain.Models.Menu;
using SimemNetAdmin.Infra.Data.Context;

namespace SimemNetAdmin.Infra.Data.Repository.Menu
{
    public class MenuRepository: IMenuRepository
    { 
        public async Task<List<MenuJsonModel>> GetRecords(string projectName)
        {
            using var context = new SimemNetAdminDbContext();
            List<MenuJsonModel> menuRecords = await context.MenuJsonModel.FromSqlInterpolated($" EXEC [menu].[prc_Menu] {projectName}").ToListAsync();
            List<MenuJsonModel> menuRecordsLst = [];

            foreach (MenuJsonModel? records in menuRecords.Where(w => w.Nivel == 1 && w.Activo))
            {
                if (HasChildren(menuRecords, records.Id))
                    records.Children = GetMenuChildrens(menuRecords, records.Id, records.Enlace);

                menuRecordsLst.Add(records);
            }

            return menuRecordsLst;
        }
     
        private static List<dynamic> GetMenuChildrens(List<MenuJsonModel> menuLst, Guid menuId, string enlace)
        {
            List<dynamic> menuChildrens = [];
            foreach (var children in menuLst.Where(w => w.IdPadre == menuId))
            {
                children.Enlace = enlace + children.Enlace;
                if (HasChildren(menuLst, children.Id))
                    children.Children = GetMenuChildrens(menuLst, children.Id, children.Enlace);

                menuChildrens.Add(children);
            }

            return menuChildrens;
        }

        private static bool HasChildren(List<MenuJsonModel> menuLst, Guid menuId)
        {
            if (menuLst.Any(w => w.IdPadre == menuId))
                return true;
            return false;
        }
       
    }
}
