#!/bin/bash
# pushes the framework to github

for D in `find ~/NikFramework/ -depth 1 -type d`
do
    cd ${D}
	name=$(basename $(pwd) | awk '{print tolower($0)}')
	echo "-----------"
    echo $name

    # rm "$name".sublime-project
    # cat ../sample.sublime-project >> "$name".sublime-project
    # rm "$name".sublime-workspace

	# git rm "$name".sublime-workspace
	# git commit -m 'removed sublime workspace file'
	# git push origin master

	# touch .gitignore
	# echo "*.sublime-workspace" >> .gitignore
	# git add .gitignore
	git status
	# git add "$name".sublime-project
	# git commit -m 'fixed sublime project files'
	# git commit -m 'gitignore'
	# git push origin master

done