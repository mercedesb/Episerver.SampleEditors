define(
    "makingwaves/editors/AutocompleteList",
    [
        "dojo/_base/array",
        "dojo/_base/declare",
        "dojo/_base/lang",
        "dojo/_base/json",
        "dojo/query",
        "dojo/dom-construct",
        "dojo/on",
		"dojo/store/JsonRest",
        "dijit/focus",
        "dijit/_TemplatedMixin",
        "dijit/_Widget",
		"dijit/form/CheckBox",
        "dijit/form/Button",
        "epi/shell/widget/_ValueRequiredMixin",
		"epi/shell/form/AutoCompleteSelectionEditor"
    ],
    function (array, declare, lang, json, query, domConstruct, on, jsonRest, focus, templatedMixin, widget, checkbox, button, valueRequiredMixin, autocomplete) {
    	return declare([widget, templatedMixin, valueRequiredMixin], {
    		templateString: "<div data-dojo-attach-point=\"stateNode, tooltipNode\" class=\"dijit dijitReset dijitInline\"> \
									<div class=\"defaultHeader\">${valueLabel}</div> \
                                <div data-dojo-attach-point=\"stringListItemsNode\" class=\"dijit dijitReset\"></div> \
                                <div class=\"dijit dijitReset\"> \
                                    <button data-dojo-attach-event=\"onclick:addStringItem\" type=\"button\" class=\"\">${addButtonLabel}</button> \
                                </div> \
                            </div>",
    		baseClass: "autocompleteItems",
    		valueLabel: "Value",
    		addButtonLabel: "+",
    		removeButtonLabel: "X",
			storeurl : "",
    		valueIsCsv: true,
    		valueIsInclusive: true,
    		value: null,
    		widgetsInTemplate: true,
    		constructor: function () {
    			this._checkboxes = [];
    			this._labelClickHandles = [];

    			this._stringItems = [];
    		},
    		postMixInProperties: function () {
    			this.inherited(arguments);

    			if (this.params.valueLabel)
    				this.valueLabel = this.params.valueLabel;

    			if (this.params.addButtonLabel)
    				this.addButtonLabel = this.params.addButtonLabel;

    			if (this.params.removeButtonLabel)
    				this.removeButtonLabel = this.params.removeButtonLabel;

    			if (this.params.storeurl)
    				this.storeurl = this.params.storeurl;


    		},
    		destroy: function () {
    			var _a;
    			while (_a = this._stringItems.pop()) {
    				_a.div.destroyRecursive();
    			}
    			this.inherited(arguments);
    		},
    		focus: function () {
    			try {
    				if (this._stringItems.length > 0) {
    					focus.focus(this._stringItems[0].div.stringListItemsNode);
    				}
    			} catch (e) {
    			}
    		},
    		onChange: function () { },
    		onBlur: function () { },
    		onFocus: function () { },
    		isValid: function () {
    			var isValid = true;
    			array.forEach(this._stringItems, function (entry) {
    				var valueTextbox = entry.valueTextbox;

    				isValid = isValid && valueTextbox.isValid();
    			});
    			return isValid;
    		},
    		_calculateValue: function () {
    			var value = [];
    			array.forEach(this._stringItems, function (entry) {
    				var valueTextbox = entry.valueTextbox;

    				if (valueTextbox.value && valueTextbox.isValid()) {
    					var item = valueTextbox.value;
    					value.push(item);
    				}
    			});

    			this.onFocus();
    			this._set("value", value);
    			if (this._started && this.validate()) {

    				// Trigger change event
    				this.onChange(value);
    			}
    		},
    		_setValueAttr: function (value) {
    			this._set("value", value);

    			array.forEach(value, this._addStringTextboxesForItem, this); // Add 'this' to end to make sure the 'this.domNode' works in method. Why?!
    		},
    		_onBlur: function () {
    			this.inherited(arguments);
    			this.onBlur();
    		},
    		addStringItem: function () {
    			this._addStringTextboxesForItem("");
    		},
    		_addStringTextboxesForItem: function (stringItem) {
    			var div = domConstruct.create("div", null, this.stringListItemsNode);
    			div.setAttribute("class", "stringListItemContainer");

    			var valueTextbox = this._getTextbox(stringItem, "valueTextbox autocompleteTextbox", this.storeurl);

    			valueTextbox.placeAt(div);

    			var btn = new button({
    				label: this.removeButtonLabel,
    				main: this,
    				container: div
    			});
    			btn.on("click", function () {
    				this.main._removeStringListItem(this.container);
    				domConstruct.destroy(this.container);
    				this.main._calculateValue();
    				this.main.onChange(this.main.value);

    			});
    			btn.placeAt(div);

    			this._pushStringListItem(div, valueTextbox);
    		},
    		_removeStringListItem: function (div) {
    			var newStringListItems = [];

    			array.forEach(this._stringItems, function (entry) {
    				if (entry.div != div) {
    					newStringListItems.push(entry);
    				}
    			});

    			this._stringItems = newStringListItems;
    		},
    		_pushStringListItem: function (div, valueTextbox) {
    			var o = new Object();
    			o.div = div;
    			o.valueTextbox = valueTextbox;

    			this._stringItems.push(o);
    		},
    		_getTextbox: function (value, cssClass, storeurl) {
    			var tb = new autocomplete({
    				value: value,
					storeurl: storeurl
    			});
    			tb.setAttribute("class", cssClass);

    			tb.on("change", lang.hitch(this, function () {
    				this._calculateValue();
    				this.onChange(this.value);
    			}));
    			tb.on("focus", lang.hitch(this, function () {
    				this._set("focused", true);
    				this.onFocus();
    			}));
    			tb.on("blur", lang.hitch(this, function () {
    				this._set("focused", false);
    				this._onBlur();
    			}));

    			return tb;
    		}
    	});
    });
