using System.Collections.Generic;
using System.Linq;

namespace DorllyService.Common.Extensions
{
    /// <summary>
    /// Author:feneng
    /// </summary>
    public static class ExtendIEnumerable
    {
        /// <summary>
        /// 转换为树形结构 Convert to tree struct
        /// 子节点集合作为父节点属性 child node as a property to parent node
        /// </summary>
        /// <typeparam name="T">IEnumerable</typeparam>
        /// <param name="enumer">the enumer</param>
        /// <param name="parentId">the id which as root node to start</param>
        /// <returns></returns>
        public static ICollection<T> ToTreeStruct<T>(this IEnumerable<T> enumer, int? parentId)
            where T : class, new()
        {
            List<T> treeEnumer = new List<T>();
            foreach (var item in enumer)
            {
                if ((dynamic)typeof(T).GetProperty("ParentId").GetValue(item) == parentId)
                {
                    var children = enumer.ToTreeStruct((int)typeof(T).GetProperty("Id").GetValue(item));
                    typeof(T).GetProperty("Children").SetValue(item, children);
                    treeEnumer.Add(item);
                }
            }
            return treeEnumer;
        }

        /// <summary>
        /// 转换为树形列表 Convert to tree list
        /// 子节点作为平级的列表元素紧跟在父节点之后
        /// 此转换符合jQuery.treetable.js要求的格式
        /// </summary>
        /// <typeparam name="T">IEnumerable</typeparam>
        /// <param name="enumer">the enumer</param>
        /// <param name="parentId">the id which as root node to start</param>
        /// <returns></returns>
        public static IEnumerable<T> ToTreeList<T>(this IEnumerable<T> enumer, int? parentId)
            where T : class, new()
        {
            var treeList = new List<T>();
            var list = enumer.Where(e => (dynamic)typeof(T).GetProperty("ParentId").GetValue(e) == parentId);
            if (list==null)
            {
                return treeList;
            }

            treeList.AddRange(list);
            foreach (var item in list)
            {
                var index= treeList.IndexOf(item);
                var children = enumer.ToTreeList<T>((int)typeof(T).GetProperty("Id").GetValue(item));
                treeList.InsertRange(index+1,children);
            }
            return treeList;
        }
    }
}
