using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using word = Microsoft.Office.Interop.Word;

namespace power_project
{
    class Report
    {

        private object _template;
        private object _newWord;
        private Microsoft.Office.Interop.Word.Application wordApp;
        private Microsoft.Office.Interop.Word.Document _wordDocument;
        private object defaultV = System.Reflection.Missing.Value;
        private object documentType;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="template">模板文件位置</param>
        /// <param name="newWord">保存位置</param>
        public Report(string template, string newWord)
        {
            this._template = template;
            this._newWord = newWord;
            wordApp = new Application();
            documentType =  Microsoft.Office.Interop.Word.WdDocumentType.wdTypeDocument;
            _wordDocument = wordApp.Documents.Add(ref _template, ref defaultV, ref documentType, ref defaultV);
        }
        /// <summary>
        /// 设置默认一页行数
        /// </summary>
        /// <param name="size"></param>
        public void SetLinesPage(int size)
        {
            wordApp.ActiveDocument.PageSetup.LinesPage = 40;
        }
        /// <summary>
        /// 设置书签的值
        /// </summary>
        /// <param name="markName">书签名</param>
        /// <param name="markValue">书签值</param>
        public void SetBookMark(string markName, string markValue)
        {
            object _markName = markName;
            try
            {
                _wordDocument.Bookmarks.get_Item(ref _markName).Range.Text = markValue;
            }
            catch
            {
                throw new Exception(markName + "未找到!!");
            }
        }
        /// <summary>
        /// 设置添加页眉
        /// </summary>
        /// <param name="context">内容</param>
        public void SetPageHeader(string context)
        {
            wordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            wordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            wordApp.ActiveWindow.ActivePane.Selection.InsertAfter(context);
            wordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            //跳出页眉设置    
            wordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;
        }
        /// <summary>
        /// 当前位置处插入文字
        /// </summary>
        /// <param name="context">文字内容</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="fontBold">粗体</param>
        /// <param name="familyName">字体</param>
        /// <param name="align">对齐方向</param>
        public void InsertText(string context, int fontSize, WdColor fontColor, int fontBold, string familyName, WdParagraphAlignment align)
        {
            //设置字体样式以及方向    
            wordApp.Application.Selection.Font.Size = fontSize;
            wordApp.Application.Selection.Font.Bold = fontBold;
            wordApp.Application.Selection.Font.Color = fontColor;
            wordApp.Selection.Font.Name = familyName;
            wordApp.Application.Selection.ParagraphFormat.Alignment = align;
            wordApp.Application.Selection.TypeText(context);

        }
        /// <summary>
        /// 翻页
        /// </summary>
        public void ToNextPage()
        {
            object breakPage = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            wordApp.Selection.InsertBreak(ref breakPage);
        }
        /// <summary>
        /// 焦点移动count段落
        /// </summary>
        /// <param name="count"></param>
        public void MoveParagraph(int count)
        {
            object _count = count;
            object wdP = WdUnits.wdParagraph;//换一段落
            wordApp.Selection.Move(ref wdP, ref _count);
        }
        /// <summary>
        /// 焦点移动count行
        /// </summary>
        /// <param name="count"></param>
        public void MoveRow(int count)
        {
            object _count = count;
            object WdLine = WdUnits.wdLine;//换一行
            wordApp.Selection.Move(ref WdLine, ref _count);
        }
        /// <summary>
        /// 焦点移动字符数
        /// </summary>
        /// <param name="count"></param>
        public void MoveCharacter(int count)
        {
            object _count = count;
            object wdCharacter = WdUnits.wdCharacter;
            wordApp.Selection.Move(ref wdCharacter, ref _count);
        }
        /// <summary>
        /// 插入段落
        /// </summary>
        public void ToNextParagraph()
        {
            wordApp.Selection.TypeParagraph();//插入段落
        }

        /// <summary>
        /// 回车换行
        /// </summary>
        public void ToNextLine()
        {
            wordApp.Selection.TypeParagraph();
        }
        /// <summary>
        /// 当前位置插入图片
        /// </summary>
        /// <param name="picture"></param>
        public void InsertPicture(string picture)
        {
            //图片居中显示    
            wordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            wordApp.Application.Selection.InlineShapes.AddPicture(picture, ref defaultV, ref defaultV, ref defaultV);
        }
        /// <summary>
        /// 添加表格
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="cellNum"></param>
        /// <returns></returns>
        public Table CreatTable(int rowNum, int cellNum)
        {
            return this._wordDocument.Tables.Add(wordApp.Selection.Range, rowNum, cellNum, ref defaultV, ref defaultV);
        }
        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="widths"></param>
        public void SetColumnWidth(float[] widths, Table tb)
        {
            if (widths.Length > 0)
            {
                int len = widths.Length;
                for (int i = 0; i < len; i++)
                {
                    tb.Columns[i].Width = widths[i];
                }
            }
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="cells"></param>
        public void MergeColumn(Table tb, Cell[] cells)
        {
            if (cells.Length > 1)
            {
                Cell c = cells[0];
                int len = cells.Length;
                for (int i = 1; i < len; i++)
                {
                    c.Merge(cells[i]);
                }
            }
            wordApp.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

        }
        /// <summary>
        /// 设置单元格内容
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="v"></param>
        /// <param name="align">对齐方式</param>
        public void SetCellValue(Cell _c, string v, WdParagraphAlignment align)
        {
            wordApp.Selection.ParagraphFormat.Alignment = align;
            _c.Range.Text = v;
        }

        /// <summary>
        /// 保存新文件
        /// </summary>
        public bool SaveAsWord()
        {
            object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            try
            {
                object fileFormat = WdSaveFormat.wdFormatRTF;
                _wordDocument.SaveAs(ref _newWord, ref fileFormat, ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV,
                    ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV, ref defaultV);
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                disponse();
            }
            return true;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        private void disponse()
        {
            object missingValue = Type.Missing;
            object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            _wordDocument.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
            wordApp.Application.Quit(ref defaultV, ref defaultV, ref defaultV);
            _wordDocument = null;
            wordApp = null;
        }
    }
}
