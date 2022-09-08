using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GAR.BL.Model;
using System.Data.Entity;

namespace GAR.BL.Controler
{
    public class DBControler
    {
        static DbGARContext context;
        static IQueryable<AS_MUN_HIERARCHY> listMunHierarchy;
        static IQueryable<AS_ADDR_OBJ> listAddrObj;
        static IQueryable<AS_ADDR_OBJ_PARAMS> listAddrObjParams;
        static IQueryable<AS_ADDR_OBJ_TYPES> listAddrObjTypes;
        static IQueryable<AS_STEADS> listSteads;
        static IQueryable<AS_STEADS_PARAMS> listSteadsParams;
        static IQueryable<AS_HOUSES> listHouses;
        static IQueryable<AS_HOUSES_PARAMS> listHousesParams;
        static IQueryable<AS_HOUSE_TYPES> listHousesTypes;
        static IQueryable<AS_CARPLACES> listCarplaces;
        static IQueryable<AS_CARPLACES_PARAMS> listCarplacesParams;
        static IQueryable<AS_APARTMENTS_PARAMS> listApartamentsParams;
        static IQueryable<AS_APARTMENTS> listApartaments;
        static IQueryable<AS_APARTMENT_TYPES> listApartamentsTypes;
        static IQueryable<AS_ROOMS> listRooms;
        static IQueryable<AS_ROOMS_PARAMS> listRoomsParams;
        static IQueryable<AS_ROOM_TYPES> listRoomsTypes;


        static DBControler()
        {
            context = new DbGARContext();

            listMunHierarchy = context.AS_MUN_HIERARCHY.AsNoTracking().Select(mh => mh);

            listAddrObj = context.AS_ADDR_OBJ.AsNoTracking().Select(a => a);

            listAddrObjParams = context.AS_ADDR_OBJ_PARAMS.AsNoTracking().Select(ap => ap);

            listAddrObjTypes = context.AS_ADDR_OBJ_TYPES.AsNoTracking().Select(at => at);

            listSteads = context.AS_STEADS.AsNoTracking().Select(s => s);

            listSteadsParams = context.AS_STEADS_PARAMS.AsNoTracking().Select(sp => sp);

            listHouses = context.AS_HOUSES.AsNoTracking().Select(h => h);

            listHousesParams = context.AS_HOUSES_PARAMS.AsNoTracking().Select(hp => hp);

            listHousesTypes = context.AS_HOUSE_TYPES.AsNoTracking().Select(ht => ht);

            listCarplaces = context.AS_CARPLACES.AsNoTracking().Select(c => c);

            listCarplacesParams = context.AS_CARPLACES_PARAMS.AsNoTracking().Select(cp => cp);

            listApartaments = context.AS_APARTMENTS.AsNoTracking().Select(a => a);

            listApartamentsParams = context.AS_APARTMENTS_PARAMS.AsNoTracking().Select(ap => ap);

            listApartamentsTypes = context.AS_APARTMENT_TYPES.AsNoTracking().Select(at => at);

            listRooms = context.AS_ROOMS.AsNoTracking().Select(r => r);

            listRoomsParams = context.AS_ROOMS_PARAMS.AsNoTracking().Select(rp => rp);

            listRoomsTypes = context.AS_ROOM_TYPES.AsNoTracking().Select(rt => rt);
        }

        //Получение пути объекта в зависимости от типа объекта
        public static List<string> PathToOject(string kadnum)
        {

            var listHierarchy = listMunHierarchy.Where(mh => mh.ISACTIVE == 1 && mh.NEXTID == 0 && mh.ENDDATE.Value.Year == 2079);

            if (listSteadsParams.Any(sp => sp.VALUE.Equals(kadnum)))
            {
                var listOject = listHierarchy
                    .Join(listSteadsParams, lh => lh.OBJECTID, ls => ls.OBJECTID, (lh, ls) => new { lh.PATH, ls.VALUE })
                .Where(ls => ls.VALUE == kadnum);
                return listOject.Select(lo => lo.PATH).ToList();
            }
            else if (listHousesParams.Any(hp => hp.VALUE.Equals(kadnum)))
            {
                var listOject = listHierarchy
                    .Join(listHousesParams, lh => lh.OBJECTID, hp => hp.OBJECTID, (lh, hp) => new { lh.PATH, hp.VALUE })
                .Where(hp => hp.VALUE == kadnum);
                return listOject.Select(lo => lo.PATH).ToList();
            }
            else if (listCarplacesParams.Any(cp => cp.VALUE.Equals(kadnum)))
            {
                var listOject = listHierarchy
                    .Join(listCarplacesParams, lh => lh.OBJECTID, cp => cp.OBJECTID, (lh, cp) => new { lh.PATH, cp.VALUE })
                .Where(cp => cp.VALUE == kadnum);
                return listOject.Select(lo => lo.PATH).ToList();
            }
            else if (listApartamentsParams.Any(ap => ap.VALUE.Equals(kadnum)))
            {
                var listOject = listHierarchy
                    .Join(listApartamentsParams, lh => lh.OBJECTID, ap => ap.OBJECTID, (lh, ap) => new { lh.PATH, ap.VALUE })
                .Where(ap => ap.VALUE == kadnum);
                return listOject.Select(lo => lo.PATH).ToList();
            }
            else if (context.AS_ROOMS_PARAMS.Any(rp => rp.VALUE.Equals(kadnum)))
            {
                var listOject = listHierarchy
                    .Join(listRoomsParams, lh => lh.OBJECTID, rp => rp.OBJECTID, (lh, rp) => new { lh.PATH, rp.VALUE })
                .Where(rp => rp.VALUE == kadnum);
                return listOject.Select(lo => lo.PATH).ToList();
            }
            else return new List<string>();
        }

        //Разложение пути в массив
        public static List<List<string>> PathToAdress(List<string> listOjects)
        {
            List<List<string>> listPathOject = new List<List<string>>();

            foreach (var item in listOjects)
            {
                List<string> list = new List<string>();
                list.AddRange(item.Split('.'));
                listPathOject.Add(list);
            }

            return listPathOject;
        }

        //Получение адреса
        public static List<string> AdressToObject(List<List<string>> listPathOject)
        {
            List<string> adresslist = new List<string>();

            string adress = "";


            foreach (var itemPathOject in listPathOject)
            {
                foreach (var itemPath in itemPathOject)
                {

                    if (listAddrObjParams.Any(ap => ap.OBJECTID.ToString().Equals(itemPath) && ap.TYPEID == 16))
                    {
                        adress = listAddrObjParams
                            .Where(ap => ap.OBJECTID.ToString().Equals(itemPath) && ap.TYPEID == 16)
                            .Select(ap => ap.VALUE).FirstOrDefault();
                    }
                    else if (listAddrObj.Any(a => a.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listAddrObj.Where(ao => ao.OBJECTID.ToString().Equals(itemPath) 
                            && (ao.NEXTID == 0 || ao.NEXTID == null)
                            && ao.ISACTIVE == 1 && ao.ISACTUAL == 1).Join(listAddrObjTypes, ao => ao.TYPENAME, at => at.SHORTNAME,
                            (ao, at) => new 
                            { 
                                objectName = ao.NAME,
                                objectLevel = ao.LEVEL,
                                typeName = at.NAME,
                                typeLevel = at.LEVEL    
                            }).Where(a => a.objectLevel == a.typeLevel);

                        adress = String.Concat(adress, ", ", itemAdress.Select(a => a.typeName).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(a => a.objectName).FirstOrDefault());
                    }
                    if (listSteads.Any(s => s.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listSteads.Where(i => i.OBJECTID.ToString().Equals(itemPath)
                            && (i.NEXTID == 0 || i.NEXTID == null)
                            && i.ISACTIVE == 1 && i.ISACTUAL == 1);

                        adress = String.Concat(adress, ", земельный участок ", itemAdress.Select(a => a.NUMBER).FirstOrDefault());
                    }
                    if (listHouses.Any(h => h.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listHouses.Where(h => h.OBJECTID.ToString().Equals(itemPath)
                            && (h.NEXTID == 0 || h.NEXTID == null)
                            && h.ISACTIVE == 1 && h.ISACTUAL == 1)
                            .Join(listHousesTypes, h => (int)h.HOUSETYPE, ht => ht.ID,
                            (h, ht) => new 
                            {
                                houseNumber = h.HOUSENUM, 
                                typeName = ht.NAME,
                            });

                        adress = String.Concat(adress, ", ", itemAdress.Select(a => a.typeName).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(a => (a.houseNumber)).FirstOrDefault());

                    }
                    if (listHouses.Any(h => h.OBJECTID.ToString().Equals(itemPath) && h.ADDNUM1 != null))
                    {
                        var itemAdress = listHouses.Where(h => h.OBJECTID.ToString().Equals(itemPath)
                            && (h.NEXTID == 0 || h.NEXTID == null)
                            && h.ISACTIVE == 1 && h.ISACTUAL == 1)
                            .Join(listHousesTypes, h => (int)h.ADDTYPE1, at => at.ID,
                            (h, at) => new
                            {
                                houseAddNum1 = h.ADDNUM1,
                                at.NAME
                            });

                        adress = String.Concat(adress, ", ", itemAdress.Select(a => a.NAME).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(a => (a.houseAddNum1)).FirstOrDefault());
                    }
                    if (listHouses.Any(h => h.OBJECTID.ToString().Equals(itemPath) && h.ADDNUM2 != null))
                    {
                        var itemAdress = listHouses.Where(i => i.OBJECTID.ToString().Equals(itemPath)
                            && (i.NEXTID == 0 || i.NEXTID == null)
                            && i.ISACTIVE == 1 && i.ISACTUAL == 1)
                            .Join(listHousesTypes, h => (int)h.ADDTYPE2, at => at.ID,
                            (h, at) => new
                            {
                                houseAddNum1 = h.ADDNUM2,
                                at.NAME
                            });

                        adress = String.Concat(adress, ", ", itemAdress.Select(a => a.NAME).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(a => (a.houseAddNum1)).FirstOrDefault());
                    }
                    if (listApartaments.Any(a => a.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listApartaments.Where(i => i.OBJECTID.ToString().Equals(itemPath)
                            && (i.NEXTID == 0 || i.NEXTID == null)
                            && i.ISACTIVE == 1 && i.ISACTUAL == 1)
                            .Join(listApartamentsTypes, a => (int)a.APARTTYPE, at => at.ID,
                            (a, at) => new
                            {
                                typeName = at.NAME,
                                apartNumber = a.NUMBER
                                
                            });

                        adress = String.Concat(adress, ", ", itemAdress.Select(at => at.typeName).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(a => (a.apartNumber)).FirstOrDefault());
                    }
                    if (listRooms.Any(a => a.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listRooms.Where(i => i.OBJECTID.ToString().Equals(itemPath)
                            && (i.NEXTID == 0 || i.NEXTID == null)
                            && i.ISACTIVE == 1 && i.ISACTUAL == 1).Join(listRoomsTypes, r => (int)r.ROOMTYPE, rt => rt.ID,
                            (r, rt) => new
                            {
                                typeName = rt.NAME,
                                roomNumber = r.NUMBER

                            });

                        adress = String.Concat(adress, ", ", itemAdress.Select(rt => rt.typeName).FirstOrDefault().ToLower(),
                            " ", itemAdress.Select(r => (r.roomNumber)).FirstOrDefault());
                    }
                    if (listCarplaces.Any(a => a.OBJECTID.ToString().Equals(itemPath)))
                    {
                        var itemAdress = listCarplaces.Where(i => i.OBJECTID.ToString().Equals(itemPath)
                            && (i.NEXTID == 0 || i.NEXTID == null)
                            && i.ISACTIVE == 1 && i.ISACTUAL == 1);

                        adress = String.Concat(adress, ", машино-место ", itemAdress.Select(c => c.NUMBER).FirstOrDefault());
                    }
                }
                adresslist.Add(adress);
            }
            return adresslist;

        }
    }
}
