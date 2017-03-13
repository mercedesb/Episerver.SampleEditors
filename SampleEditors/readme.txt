---------------------------------------------
-------- Sample Editors Nuget Readme --------
---------------------------------------------

If you plan to use the custom dojo editors in this package, please make the following updates to your module.config:

---- Add a path to the Styles.css

<clientResources>
	<add name="epi-cms.widgets.base" path="Styles/Styles.css" resourceType="Style"/>
</clientResources>

---- Add a mapping from site to ~/ClientResources/Scripts to the dojo loader configuration

<dojo>
	<paths>
		<add name="makingwaves" path="Scripts" />
	</paths>
</dojo>
