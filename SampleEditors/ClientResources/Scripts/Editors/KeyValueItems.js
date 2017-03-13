define(
    "makingwaves/editors/KeyValueItems",
    [
        "dojo/_base/array",
        "dojo/_base/declare",
        "dojo/_base/lang",
        "dojo/_base/json",
        "dojo/query",
        "dojo/dom-construct",
        "dojo/on",
        "dijit/focus",
        "dijit/_TemplatedMixin",
        "dijit/_Widget",
        "dijit/form/ValidationTextBox",
		"dijit/form/CheckBox",
        "dijit/form/Button",
        "epi/shell/widget/_ValueRequiredMixin"
    ],
    function (array, declare, lang, json, query, domConstruct, on, focus, templatedMixin, widget, textbox, checkbox, button, valueRequiredMixin) {
    	return declare([widget, templatedMixin, valueRequiredMixin], {
    		templateString: "<div data-dojo-attach-point=\"stateNode, tooltipNode\" class=\"dijit dijitReset dijitInline\"> \
									<div class=\"defaultHeader\">${keyLabel}</div> \
									<div class=\"defaultHeader\">${valueLabel}</div> \
									<div class=\"defaultHeader\" style=\"display:${selectedCheckboxDisplay};\">Selected by Default</div> \
                                <div data-dojo-attach-point=\"keyValueItemsNode\" class=\"dijit dijitReset\"></div> \
                                <div class=\"dijit dijitReset\"> \
                                    <button data-dojo-attach-event=\"onclick:addKeyValueItem\" type=\"button\" class=\"\">${addButtonLabel}</button> \
                                </div> \
                            </div>",
    		baseClass: "keyValueItems",
    		keyLabel: "Key",
    		valueLabel: "Value",
    		showSelectedByDefaultCheckbox: false,
			selectedCheckboxDisplay: 'none',
    		addButtonLabel: "Add",
    		removeButtonLabel: "X",
    		keyValidationExpression: "",
    		keyValidationMessage: "",
    		valueValidationExpression: "",
    		valueValidationMessage: "",
    		valueIsCsv: true,
    		valueIsInclusive: true,
    		value: null,
    		widgetsInTemplate: true,
    		constructor: function () {
    			this._checkboxes = [];
    			this._labelClickHandles = [];

    			this._keyValueItems = [];
    		},
    		postMixInProperties: function () {
    			this.inherited(arguments);

    			if (this.params.keyLabel)
    				this.keyLabel = this.params.keyLabel;

    			if (this.params.valueLabel)
    				this.valueLabel = this.params.valueLabel;

    			if (this.params.addButtonLabel)
    				this.addButtonLabel = this.params.addButtonLabel;

    			if (this.params.removeButtonLabel)
    				this.removeButtonLabel = this.params.removeButtonLabel;

    			if (this.params.keyValidationExpression)
    				this.keyValidationExpression = this.params.keyValidationExpression;

    			if (this.params.keyValidationMessage)
    				this.keyValidationMessage = this.params.keyValidationMessage;

    			if (this.params.valueValidationExpression)
    				this.valueValidationExpression = this.params.valueValidationExpression;

    			if (this.params.valueValidationMessage)
    				this.valueValidationMessage = this.params.valueValidationMessage;

    			if (typeof this.params.showSelectedByDefaultCheckbox !== 'undefined' && this.params.showSelectedByDefaultCheckbox !== null) {
    				if (this.params.showSelectedByDefaultCheckbox) {
    					this.selectedCheckboxDisplay = 'inline-block';
    					this.showSelectedByDefaultCheckbox = this.params.showSelectedByDefaultCheckbox;
    				}
    					
    			}
    		},
    		destroy: function () {
    			var _a;
    			while (_a = this._keyValueItems.pop()) {
    				_a.div.destroyRecursive();
    			}
    			this.inherited(arguments);
    		},
    		focus: function () {
    			try {
    				if (this._keyValueItems.length > 0) {
    					focus.focus(this._keyValueItems[0].div.keyValueItemsNode);
    				}
    			} catch (e) {
    			}
    		},
    		onChange: function () { },
    		onBlur: function () { },
    		onFocus: function () { },
    		isValid: function () {
    			var isValid = true;
    			array.forEach(this._keyValueItems, function (entry) {
    				var keyTextbox = entry.keyTextbox,
                        valueTextbox = entry.valueTextbox;

    				isValid = isValid && keyTextbox.isValid() && valueTextbox.isValid();
    			});
    			return isValid;
    		},
    		_calculateValue: function () {
    			var value = [];
    			array.forEach(this._keyValueItems, function (entry) {
    				var keyTextbox = entry.keyTextbox,
                        valueTextbox = entry.valueTextbox,
    					selectedCheckbox = entry.selectedCheckbox;

    				if (keyTextbox.value && valueTextbox.value && keyTextbox.isValid() && valueTextbox.isValid()) {
    					var keyValuePair = new Object();
    					keyValuePair.key = keyTextbox.value;
    					keyValuePair.value = valueTextbox.value;
    					keyValuePair.selectedByDefault = !!selectedCheckbox ? selectedCheckbox.checked : false;
    					value.push(keyValuePair);
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

    			array.forEach(value, this._addKeyValueTextboxesForItem, this); // Add 'this' to end to make sure the 'this.domNode' works in method. Why?!
    		},
    		_onBlur: function () {
    			this.inherited(arguments);
    			this.onBlur();
    		},
    		addKeyValueItem: function () {
    			this._addKeyValueTextboxesForItem({ "Key": "", "Value": "", "SelectedByDefault": false });
    		},
    		_addKeyValueTextboxesForItem: function (keyValueItem) {
    			var div = domConstruct.create("div", null, this.keyValueItemsNode);
    			div.setAttribute("class", "keyValueItemContainer");

    			var keyTextbox = this._getTextbox(keyValueItem.key, "keyTextbox", this.keyValidationMessage, this.keyValidationExpression);
    			var valueTextbox = this._getTextbox(keyValueItem.value, "valueTextbox", this.valueValidationMessage, this.valueValidationExpression);
    			var selectedCheckbox = null;
    			if (this.showSelectedByDefaultCheckbox) {
    				selectedCheckbox = this._getCheckBox(keyValueItem.selectedByDefault, "selectedByDefaultCheckbox");
    			}

    			keyTextbox.placeAt(div);
    			valueTextbox.placeAt(div);
    			if (selectedCheckbox !== null) {
    				selectedCheckbox.placeAt(div);
    			}

    			var btn = new button({
    				label: this.removeButtonLabel,
    				main: this,
    				container: div
    			});
    			btn.on("click", function () {
    				this.main._removeKeyValueItem(this.container);
    				domConstruct.destroy(this.container);
    				this.main._calculateValue();
    				this.main.onChange(this.main.value);

    			});
    			btn.placeAt(div);

    			this._pushKeyValueItem(div, keyTextbox, valueTextbox, selectedCheckbox);
    		},
    		_removeKeyValueItem: function (div) {
    			var newKeyValueItems = [];

    			array.forEach(this._keyValueItems, function (entry) {
    				if (entry.div != div) {
    					newKeyValueItems.push(entry);
    				}
    			});

    			this._keyValueItems = newKeyValueItems;
    		},
    		_pushKeyValueItem: function (div, keyTextbox, valueTextbox, selectedCheckbox) {
    			var o = new Object();
    			o.div = div;
    			o.keyTextbox = keyTextbox;
    			o.valueTextbox = valueTextbox;
    			o.selectedCheckbox = selectedCheckbox;

    			this._keyValueItems.push(o);
    		},
    		_getTextbox: function (value, cssClass, message, expression) {
    			var tb = new textbox({
    				value: value,
    				invalidMessage: message,
    				regExp: expression
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
    		},
    		_getCheckBox: function (isChecked, cssClass) {
    			var cb = new checkbox({
    				checked: isChecked
    			});
    			cb.setAttribute("class", cssClass);

    			cb.on("change", lang.hitch(this, function () {
    				this._calculateValue();
    				this.onChange(this.value);
    			}));
    			cb.on("focus", lang.hitch(this, function () {
    				this._set("focused", true);
    				this.onFocus();
    			}));
    			cb.on("blur", lang.hitch(this, function () {
    				this._set("focused", false);
    				this._onBlur();
    			}));

    			return cb;
    		}
    	});
    });
