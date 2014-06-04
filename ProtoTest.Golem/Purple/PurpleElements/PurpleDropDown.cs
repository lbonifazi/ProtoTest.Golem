﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace ProtoTest.Golem.Purple.PurpleElements
{
    public class PurpleDropDown : PurpleElementBase
    {
        private AutomationElementCollection availableOptions;

        public PurpleDropDown(string name, string locatorPath) : base(name, locatorPath)
        {
        }

        //change to get selected item
        public string GetSelected()
        {
            string nameofSelected = "";
            if (_UIAElement != null)
            {
                object basePattern;
                if (_UIAElement.TryGetCurrentPattern(SelectionPattern.Pattern, out basePattern))
                {
                    SelectionPattern selection = (BasePattern) basePattern as SelectionPattern;
                    if (selection != null)
                    {
                        AutomationElement[] automationElements = selection.Current.GetSelection();
                        foreach (var automationElement in automationElements)
                        {
                            nameofSelected = automationElement.Current.Name;
                        }
                    }
                }
            }
            else
            {
                if (PurpleElement.Current.IsEnabled)
                {
                    GetSelected();
                }
            }
            return nameofSelected;
        }

        public void SelectItem(string item)
        {
            if (_UIAElement != null)
            {
                //Now we need to find the right one
                int matchingIndex = -1;
                AutomationElement itemToSelect = null;

                object basePattern;
                if (_UIAElement.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out basePattern))
                {
                    ExpandCollapsePattern expand = (BasePattern) basePattern as ExpandCollapsePattern;
                    if (expand != null)
                    {
                        if (expand.Current.ExpandCollapseState == ExpandCollapseState.Collapsed)
                        {
                            expand.Expand();

                            availableOptions = _UIAElement.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
                            for (int x = 0; x < availableOptions.Count; x++)
                            {
                                if (item == availableOptions[x].Current.Name)
                                {
                                    itemToSelect = availableOptions[x];
                                }
                            }
                            if (itemToSelect != null)
                            {
                                SelectionItemPattern selectPattern = (SelectionItemPattern) itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                                selectPattern.Select();
                            }
                        }
                    }
                }
            }
            else
            {
                if (PurpleElement.Current.IsEnabled)
                {
                    SelectItem(item);
                }
            }
        }

    }
}
