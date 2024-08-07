using Cysharp.Threading.Tasks.Triggers;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class UIRecycleViewController<T> : MonoBehaviour
    {
        public RectTransform CachedRectTransform => GetComponent<RectTransform>();
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

        protected List<T> tableData = new List<T>();
        [SerializeField] protected GameObject cellBase = null;
        [SerializeField] float spacingHeight = 4.0f;
        [SerializeField] RectOffset padding;
        [SerializeField] RectOffset visibleRectPadding = null;
        LinkedList<UIRecycleViewCell<T>> cells = new LinkedList<UIRecycleViewCell<T>>();
        Rect visibleRect;
        Vector2 prevScrollPos;

        protected virtual void Start()
        {
            cellBase.SetActive(false);
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
        }

        protected void InitializeTableView()
        {
            UpdateScrollViewSize();
            UpdateVisibleRect();

            if (cells.Count < 1)
            {
                Vector2 cellTop = new Vector2(0.0f, -padding.top);
                for (int i = 0; i < tableData.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i);
                    Vector2 cellBottom = cellTop + new Vector2(0.0f, -cellHeight);

                    if ((cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height) ||
                        (cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height))
                    {
                        UIRecycleViewCell<T> cell = CreateCellForIndex(i);
                        cell.Top  = cellTop;
                        break;
                    }

                    cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
                }

                SetFillVisibleRectWithCells();
            }
            else
            {
                LinkedListNode<UIRecycleViewCell<T>> node = cells.First;
                UpdateCellForIndex(node.Value, node.Value.Index);
                node = node.Next;

                while (node != null)
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                    node = node.Next;
                }

                SetFillVisibleRectWithCells();
            }
        } 
       
        protected virtual float GetCellHeightAtIndex(int index)
        {
            return cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        protected void UpdateScrollViewSize()
        {
            float contentHeight = 0.0f;
            for (int i = 0; i < tableData.Count; i++)
            {
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    contentHeight += spacingHeight;
                }
            }

            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = padding.top + contentHeight + padding.bottom;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        UIRecycleViewCell<T> CreateCellForIndex(int index)
        {
            GameObject obj = Instantiate(cellBase,cellBase.transform.parent) as GameObject;
            obj.SetActive(true);
            UIRecycleViewCell<T> cell = obj.GetComponent<UIRecycleViewCell<T>>();
            UpdateCellForIndex(cell, index);
            cells.AddLast(cell);

            return cell;
        }

        void UpdateCellForIndex(UIRecycleViewCell<T> cell, int index)
        {
            cell.Index = index;

            if (cell.Index >= 0 && cell.Index <= tableData.Count - 1)
            {
                cell.gameObject.SetActive(true);
                cell.UpdateContent(tableData[cell.Index]);
                cell.Height = GetCellHeightAtIndex(cell.Index);
            }
            else
            {
                cell.gameObject.SetActive(false);
            }
        }

        void UpdateVisibleRect()
        {
            visibleRect.x = CachedScrollRect.content.anchoredPosition.x + visibleRectPadding.left;
            visibleRect.y = -CachedScrollRect.content.anchoredPosition.y + visibleRectPadding.top;
            visibleRect.width = CachedRectTransform.rect.width + visibleRectPadding.left + visibleRectPadding.right;
            visibleRect.height = CachedRectTransform.rect.height + visibleRectPadding.top + visibleRectPadding.bottom;
        }

        void SetFillVisibleRectWithCells()
        {
            if (cells.Count < 1)
            {
                return;
            }

            UIRecycleViewCell<T> lastCell = cells.Last.Value;
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

            while (nextCellDataIndex < tableData.Count && nextCellTop.y >= visibleRect.y - visibleRect.height)
            {
                UIRecycleViewCell<T> cell = CreateCellForIndex(nextCellDataIndex);
                cell.Top = nextCellTop;
                lastCell = cell;
                nextCellDataIndex = lastCell.Index + 1;
                nextCellTop = lastCell.Bottom + new Vector2(0.0f ,-spacingHeight);
            }
        }

        public void OnScrollPosChanged(Vector2 scrollPos)
        {
            UpdateVisibleRect();
            UpdateCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);

            prevScrollPos = scrollPos;
        }

        void UpdateCells(int scrollDirection)
        {
            if (cells.Count < 1)
            {
                return;
            }

            if (scrollDirection > 0)
            {
                UIRecycleViewCell<T> firstCell = cells.First.Value;
                while (firstCell.Bottom.y > visibleRect.y)
                {
                    UIRecycleViewCell<T> lastCell = cells.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);
                    firstCell.Top = lastCell.Bottom + new Vector2(0.0f ,-spacingHeight);

                    cells.AddLast(firstCell);
                    cells.RemoveFirst();
                    firstCell = cells.First.Value;
                }

                SetFillVisibleRectWithCells();
            }
            else if(scrollDirection < 0)
            {
                UIRecycleViewCell<T> lastCell = cells.Last.Value;
                while (lastCell.Top.y < visibleRect.y - visibleRect.height)
                {
                    UIRecycleViewCell<T> firstCell = cells.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index -1);
                    lastCell.Bottom = firstCell.Top + new Vector2(0.0f ,spacingHeight);

                    cells.AddLast(lastCell);
                    cells.RemoveLast();
                    lastCell = cells.Last.Value;
                }
            }
        }

    }
}
