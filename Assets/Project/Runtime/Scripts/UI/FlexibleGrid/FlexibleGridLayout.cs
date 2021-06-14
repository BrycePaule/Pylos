using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
	public enum FitType
	{
		Uniform,
		Width,
		Height,
		FixedRows,
		FixedColumns,
	}

	public FitType fitType;

	public int Rows;
	public int Columns;
	public Vector2 CellSize;
	public Vector2 Spacing;

	public bool FitX;
	public bool FitY;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
		{
			FitX = true;
			FitY = true;

			float sqrRt = Mathf.Sqrt(transform.childCount);
			Rows = Mathf.CeilToInt(sqrRt);
			Columns = Mathf.CeilToInt(sqrRt);
		}

		if (fitType == FitType.Width || fitType == FitType.FixedColumns)
		{
			Rows = Mathf.CeilToInt(transform.childCount / (float) Columns);
		}

		if (fitType == FitType.Height || fitType == FitType.FixedRows)
		{
			Columns = Mathf.CeilToInt(transform.childCount / (float) Rows);
		}

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		float cellWidth = (parentWidth / (float) Columns) - ((Spacing.x / (float) Columns) * (Columns - 1)) - (padding.left / (float) Columns) - (padding.right / (float) Columns);
		float cellHeight = (parentHeight / (float) Rows) - ((Spacing.y / (float) Rows) * (Rows - 1)) - (padding.top / (float) Rows) - (padding.bottom / (float) Rows);

		CellSize.x = FitX ? cellWidth : CellSize.x;
		CellSize.y = FitY ? cellHeight: CellSize.y;

		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / Columns;
			columnCount = i % Columns;

			var item = rectChildren[i];

			var xPos = (CellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
			var yPos = (CellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

			SetChildAlongAxis(item, 0, xPos, CellSize.x);
			SetChildAlongAxis(item, 1, yPos, CellSize.y);
		}
	}
	
	public override void CalculateLayoutInputVertical()
	{
	}

	public override void SetLayoutHorizontal()
	{
	}

	public override void SetLayoutVertical()
	{
	}
}
