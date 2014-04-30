require 'fuburake'


FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuMVC.Instrumentation.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuMVC.Instrumentation",
		:copyright => 'Copyright 2012-2013 FubuMVC.Diagnostics. All rights reserved.'
	}
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
	
	sln.assembly_bottle 'FubuMVC.Instrumentation'
	
	sln.options[:nuget_publish_folder] = 'nupkgs'
	sln.options[:nuget_publish_url] = 'https://www.myget.org/F/fubumvc-edge/'

end
