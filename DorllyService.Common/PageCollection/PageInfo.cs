using System.Collections.Generic;

namespace DorllyService.Common
{
    /// <summary>
    /// Jquery.DataTables 前后端数据类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T> where T : class
    {
        public PageInfo() { }

        public PageInfo(int questDraw, int pageIndex, int pageSize, int total, int filteredCount,  List<T> list)
        {
            this.draw = questDraw;
            this.recordsTotal = total;
            this.recordsFiltered = filteredCount;
            this.length = pageSize;
            if (filteredCount == 0)
            {
                this.start = 0;
            }
            else
            {
                int maxpage = filteredCount / pageSize;
                if (filteredCount % pageSize > 0)
                {
                    maxpage++;
                }
                if (pageIndex >= maxpage)
                {
                    this.pageIndex = maxpage;
                }
                this.start = pageSize * pageIndex - pageSize + 1;
            }
            this.data = list;
        }

        public PageInfo(int questDraw, int pageIndex, int pageSize, int total, int filteredCount, List<DataTableColumn> questColumns, List<OrderStruct> questOrder,List<T> list)
        {
            this.draw = questDraw;
            this.recordsTotal = total;
            this.recordsFiltered = filteredCount;
            this.length = pageSize;
            if (filteredCount == 0)
            {
                this.start = 0;
            }
            else
            {
                int maxpage = filteredCount / pageSize;
                if (filteredCount % pageSize > 0)
                {
                    maxpage++;
                }
                if (pageIndex >= maxpage)
                {
                    this.pageIndex = maxpage;
                }
                this.start = pageSize * pageIndex - pageSize + 1;
            }
            this.data = list;
            this.columns = questColumns;
            this.order = questOrder;
        }

        /// <summary>
        /// 列信息
        /// </summary>
        public List<DataTableColumn> columns { get; set; }

        /// <summary>
        /// 请求次数
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 过滤后的记录数
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 起始索引
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> data { get; set; }

        /// <summary>
        /// 排序规则
        /// </summary>
        public List<OrderStruct> order { get; set; }

        public string search { get; set; }
    }
}