begin
  require 'bundler/setup'
  require 'fuburake'
rescue LoadError
  puts 'Bundler and all the gems need to be installed prior to running this rake script. Installing...'
  system("gem install bundler --source http://rubygems.org")
  sh 'bundle install'
  system("bundle exec rake", *ARGV)
  exit 0
end


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
end
